using App.Persistence.Context;
using App.Persistence.Service;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Persistence.Context;
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
        public PassportChangeListener()
        {
            ModuleID = SystemModules.Passport;
        }

        public async Task ProcessChangedAsync(int RecordID, int ToProcess, int FromProcess, BaseContext baseContext)
        {
            var AppContext = (AppDbContext)baseContext;
            var cur = AppContext.PassportApplications.Where(e => e.Id == RecordID).Single();
            cur.CurProcessId = ToProcess;
            if(FromProcess == SystemProcess.Registration)
            {
                if(!AppContext.Biometrics.Where(e => e.ProfileId == cur.ProfileId).Any())
                {
                    //throw new BusinessRulesException("بایومتریک شخص صورت نگرفته است");
                }
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

            await AppContext.SaveChangesAsync();
        }

        
    }
}
