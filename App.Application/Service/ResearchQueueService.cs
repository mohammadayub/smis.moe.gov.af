using App.Domain.Entity.prc;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Service
{
    public class ResearchQueueService : IHostedService
    {
        private ILogger<ResearchQueueService> Logger { get; set; }
        private AppDbContext Context { get; set; }
        private AppIdentityDbContext IdentityDbContext { get; }
        public ResearchQueueService(ILogger<ResearchQueueService> logger, AppDbContext context,AppIdentityDbContext appIdentityDb)
        {
            Logger = logger;
            Context = context;
            IdentityDbContext = appIdentityDb;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    var Users = await IdentityDbContext.UserRoles
                        .Where(e => e.Role.Name == SystemRoles.ResearchAndControl && e.User.Disabled == false)
                        .Select(e => e.User).ToListAsync();
                    var UIDs = Users.Select(e => e.Id);
                    var UserOffices = Users.Select(e => new { e.Id, e.OfficeID });
                    if (UIDs.Any())
                    {
                        var UFiles = Context.ResearchQueues.Where(e => e.Processed == false && UIDs.Contains(e.UserId)).GroupBy(e => e.UserId)
                            .Select(e => new UserFiles { UserID = e.Key, FilesCount = e.Count() }).ToList(); ;

                        UIDs.Where(e => !UFiles.Any(eF => eF.UserID == e)).ToList().ForEach(e => UFiles.Add(new UserFiles { UserID = e, FilesCount = 0 }));

                        UFiles.ForEach(cur =>
                        {
                            var cr = UserOffices.Where(e => e.Id == cur.UserID).Single();
                            cur.OfficeID = cr.OfficeID;
                        });


                        var NRecs = Context.ProcessTracking.AsNoTracking().Where(e => e.ToUserId == null && e.ProcessId == SystemProcess.ReasearchAndControl).ToList();
                        foreach(var rec in NRecs)
                        {
                            try
                            {
                                var rid = (int)rec.RecordId;
                                await ProcessRecordAsync(rid,rec.Id, UFiles);
                            }
                            catch(Exception ex)
                            {
                                Logger.LogError("App Exception : {0}", ex);
                            }
                        }
                        Logger.LogInformation("Number Of Records Processed {0}",NRecs.Count);
                    }

                }
                catch(Exception ex)
                {
                    Logger.LogError("App Exception : {0}",ex);
                }

                await Task.Delay(10000);
            }
        }

        private async Task ProcessRecordAsync(int record,long trackid, List<UserFiles> UFiles)
        {
            var createdBy = Context.PassportApplications.Where(e => e.Id == record).Select(e => e.CreatedBy).Single();
            var officeID = IdentityDbContext.Users.Where(e => e.Id == createdBy).Select(e => e.OfficeID).Single();

             var frs = UFiles.Where(e => e.OfficeID == officeID).OrderBy(e => e.FilesCount ).FirstOrDefault();
            if(frs != null)
            {
                frs.FilesCount++;
                var track = Context.ProcessTracking.Where(e => e.Id == trackid).Single();
                track.ToUserId = frs.UserID;
                ResearchQueue rq = new ResearchQueue
                {
                    ApplicationId = record,
                    UserId = frs.UserID,
                    AssignedDate = DateTime.Now,
                    Processed = false
                };
                Context.ResearchQueues.Add(rq);

                await Context.SaveChangesAsync(track:false);

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private class UserFiles
        {
            public int UserID { get; set; }
            public int OfficeID { get; set; }
            public int FilesCount { get; set; }
        }
    }
}
