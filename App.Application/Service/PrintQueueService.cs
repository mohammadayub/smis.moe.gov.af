using App.Domain.Entity.prc;
using App.Domain.Entity.prt;
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
    public class PrintQueueService : BackgroundService
    {
        private ILogger<PrintQueueService> Logger { get; set; }
        private AppDbContext Context { get; set; }
        private AppIdentityDbContext IdentityDbContext { get; }
        public PrintQueueService(ILogger<PrintQueueService> logger, AppDbContext context,AppIdentityDbContext appIdentityDb)
        {
            Logger = logger;
            Context = context;
            IdentityDbContext = appIdentityDb;
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
                PrintQueue rq = new PrintQueue
                {
                    ApplicationId = record,
                    UserId = frs.UserID,
                    CreatedOn = DateTime.Now,
                    IsProcessed = false
                };
                Context.PrintQueues.Add(rq);

                await Context.SaveChangesAsync(track:false);

            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                try
                {
                    var UFiles = await (
                        from f in Context.UserOfficePrinters
                        join d in Context.PrintQueues on f.UserId equals d.UserId
                        into p
                        from pq in p.DefaultIfEmpty()
                        group f by new { f.UserId, f.OfficeId }

                    ).Select(e => new UserFiles { UserID = e.Key.UserId, OfficeID = e.Key.OfficeId, FilesCount = e.Count() })
                    .ToListAsync();
                    
                    if (UFiles.Any())
                    {

                        var NRecs = Context.ProcessTracking.AsNoTracking()
                            .Where(e => e.ToUserId == null && e.ProcessId == SystemProcess.Print && e.StatusId == ProcessStatus.InProcess)
                            .ToList();
                        foreach (var rec in NRecs)
                        {
                            try
                            {
                                var rid = (int)rec.RecordId;
                                await ProcessRecordAsync(rid, rec.Id, UFiles);
                            }
                            catch (Exception ex)
                            {
                                Logger.LogError("App Exception : {0}", ex);
                            }
                        }
                        Logger.LogInformation("Number Of Records Processed {0}", NRecs.Count);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError("App Exception : {0}", ex);
                }

                await Task.Delay(60000);
            }
        }

        private class UserFiles
        {
            public int UserID { get; set; }
            public int OfficeID { get; set; }
            public int FilesCount { get; set; }
        }
    }
}
