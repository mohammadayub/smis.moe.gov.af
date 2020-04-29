using App.Persistence.Context;
using App.Persistence.Service;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Persistence.Context;
using Clean.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Events
{
    public class PassportChangeListener : IProcessChangeListener
    {
        public int ModuleID { get; set; }
        private ICurrentUser CurrentUser { get; }
        public PassportChangeListener(ICurrentUser currentUser)
        {
            ModuleID = SystemModules.Passport;
            CurrentUser = currentUser;
        }

        public async Task ProcessChangedAsync(long RecordID, int ToProcess, int FromProcess, BaseContext baseContext)
        {
            var UserID = await CurrentUser.GetUserId();
            var AppContext = (AppDbContext)baseContext;
            var cur = AppContext.PassportApplications.Where(e => e.Id == RecordID).Single();
            cur.CurProcessId = ToProcess;
            if(FromProcess == SystemProcess.Registration)
            {
                var aadr = AppContext.Addresses.Where(e => e.ProfileId == cur.ProfileId && e.StatusId == 1).SingleOrDefault();
                if (aadr == null)
                {
                    throw new BusinessRulesException("آدرس متقاضی ثبت نشده است");
                }
                cur.ActiveAddressId = aadr.Id;

                var ajb = AppContext.Jobs.Where(e => e.ProfileId == cur.ProfileId && e.StatusId == 1).SingleOrDefault();
                if (ajb == null)
                {
                    throw new BusinessRulesException("وظیفه متقاضی ثبت نشده است");
                }
                cur.ActiveJobId = ajb.Id;

                var abd = AppContext.BioDatas.Where(e => e.ProfileId == cur.ProfileId && e.StatusId == 1).SingleOrDefault();
                if (abd == null)
                {
                    throw new BusinessRulesException("معلومات شخص تکمیل شده نیست");
                }
                cur.ActiveBioDataId = abd.Id;
            }
            else if(FromProcess == SystemProcess.ReasearchAndControl)
            {
                var rq = AppContext.ResearchQueues.Where(e => e.Processed == false && e.ApplicationId == RecordID && e.UserId == UserID).Single();
                rq.Processed = true;
                rq.ProcessedDate = DateTime.Now;
            }
            else if (FromProcess == SystemProcess.Authorization)
            {
                var rq = AppContext.AuthorizationQueues.Where(e => e.Processed == false && e.ApplicationId == RecordID && e.UserId == UserID).Single();
                rq.Processed = true;
                rq.ProcessedDate = DateTime.Now;
            }
            else if(FromProcess == SystemProcess.Print)
            {
                var pq = AppContext.PrintQueues.Where(e => e.IsProcessed == false && e.ApplicationId == RecordID && e.UserId == UserID).Single();
                pq.ProcessedOn = DateTime.Now;
                pq.IsProcessed = true;

                var pp = AppContext.PassportPrints.Where(e => e.PrintQueueId == pq.Id).SingleOrDefault();
                if(pp != null && pp.StatusId != PassportPrintStatus.Printed)
                {
                    throw new BusinessRulesException("این پاسپورت پرنت نشده است!");
                }

                if(pp == null && ToProcess == SystemProcess.QualityControl)
                {
                    throw new BusinessRulesException("این پاسپورت پرنت نشده است!");
                }

            }
            else if(FromProcess == SystemProcess.QualityControl)
            {
                if(ToProcess == SystemProcess.Print)
                {
                    var pp = AppContext.PassportPrints.Where(e => e.PrintQueue.ApplicationId == RecordID && e.StatusId == PassportPrintStatus.Printed)
                        .OrderByDescending(e => e.PrintedDate).FirstOrDefault();

                    var ps = AppContext.Passports.Where(e => e.Id == pp.PassportId).Single();

                    pp.StatusId = PassportPrintStatus.Spoiled;
                    ps.StatusId = PassportStatus.Spoiled;

                }
                else if(ToProcess == SystemProcess.Close)
                {
                    var app = AppContext.PassportApplications.Where(e => e.Id == RecordID).Single();
                    app.StatusId = 1;
                }
            }

            await AppContext.SaveChangesAsync();
        }

        
    }
}
