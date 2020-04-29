using App.Application.Registration.Models;
using App.Application.Registration.Queries;
using App.Domain.Entity.pas;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Common.Extensions;
using Clean.Domain.Entity.prc;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Commands
{
    public class SaveApplicationCommand : IRequest<List<PassportApplicationModel>>
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public int PassportTypeId { get; set; }
        public int PassportDurationId { get; set; }
        public int? DiscountId { get; set; }
        public int ProfileId { get; set; }
        public int PaymentCategoryId { get; set; }
        public int PaymentPenaltyId { get; set; }
        public int PaymentMethodId { get; set; }
        public double PaidAmount { get; set; }
        public string ReceiptNumer { get; set; }
        public int RequestTypeId { get; set; }
        public int? BankId { get; set; }
        public DateTime PaymentDate { get; set; }
        public long? ActiveBioDataId { get; set; }
        public int? ActiveAddressId { get; set; }
        public int? ActiveJobId { get; set; }
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; }
    }

    public class SaveApplicationCommandHandler : IRequestHandler<SaveApplicationCommand, List<PassportApplicationModel>>
    {
        private AppDbContext Context {get;set;}
        private IMediator Mediator {get;set;}
        private ICurrentUser CurrentUser {get;set;}
        public SaveApplicationCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PassportApplicationModel>> Handle(SaveApplicationCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var OfficeID = await CurrentUser.GetOfficeID();

            var pm = Context.PaymentMethods.Where(e => e.Id == request.PaymentMethodId).Single();
            if(pm.HasReceipt && String.IsNullOrEmpty(request.ReceiptNumer)){
                throw new BusinessRulesException("این نوع پرداخت ضرورت به نمبر بل دارد!");
            }
            var pconfig = Context.PaymentConfigs.Where(e => e.OfficeId == OfficeID 
                            && e.PassportTypeId == request.PassportTypeId 
                            && e.PassportDurationId == request.PassportDurationId
                            && e.PaymentCategoryId == request.PaymentCategoryId
                            && e.StatusId == 1).SingleOrDefault();
            if(pconfig == null){
                throw new BusinessRulesException("تنظیمات پرداخت برای درخواست فعلی وجود ندارد!");
            }
            
            if(request.Id.HasValue){
                var cur = Context.PassportApplications.Where(e => e.Id == request.Id).Single();
                if(cur.CurProcessId != SystemProcess.Registration){
                    throw new BusinessRulesException("این درخواست قابل تغییر نمی باشد!");
                }
                if(cur.CreatedBy != UserID){
                    throw new BusinessRulesException("شما نمی توانید این درخواست را تغییر بدهید!");
                }
                
                cur.PassportTypeId = request.PassportTypeId;
                cur.PassportDurationId = request.PassportDurationId;
                cur.PaymentCategoryId = request.PaymentCategoryId;
                cur.DiscountId = request.DiscountId;
                cur.PaymentPenaltyId = request.PaymentPenaltyId;
                cur.PaymentMethodId = request.PaymentMethodId;
                cur.PaymentDate = request.PaymentDate;
                cur.RequestTypeId = request.RequestTypeId;
                cur.PaidAmount = request.PaidAmount;
                cur.ReceiptNumer = request.ReceiptNumer;
                cur.BankId = request.BankId;
                cur.PhotoPath = request.PhotoPath;
                cur.SignaturePath = request.SignaturePath;

                var aadr = Context.Addresses.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                if(aadr == null){
                    throw new BusinessRulesException("آدرس متقاضی ثبت نشده است");
                }
                cur.ActiveAddressId = aadr.Id;

                var ajb = Context.Jobs.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                if(ajb == null){
                    throw new BusinessRulesException("وظیفه متقاضی ثبت نشده است");
                }
                cur.ActiveJobId = ajb.Id;

                var abd = Context.BioDatas.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                if(abd == null){
                    throw new BusinessRulesException("معلومات شخص تکمیل شده نیست");
                }
                cur.ActiveBioDataId = abd.Id;

                var pConfig = await Mediator.Send(new GetApplicationPaymentConfig
                {
                    ApplicationID = cur.Id,
                    PassportTypeID = cur.PassportTypeId,
                    PassportDurationID = cur.PassportDurationId,
                    PaymentCategoryID = cur.PaymentCategoryId,
                    PaymentPenaltyID = cur.PaymentPenaltyId,
                    DiscountID = cur.DiscountId
                });

                cur.PaidAmount = pConfig.Amount;

                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;

                await Context.SaveChangesAsync();

                return await Mediator.Send(new SearchApplicationQuery{ ID = cur.Id});

            }
            else{
                using(var trans = Context.Database.BeginTransaction())
                {
                    try
                    {
                        var cur = new Domain.Entity.pas.PassportApplication
                        {
                            ProfileId = request.ProfileId,

                            PassportTypeId = request.PassportTypeId,
                            PassportDurationId = request.PassportDurationId,
                            PaymentCategoryId = request.PaymentCategoryId,
                            DiscountId = request.DiscountId,
                            PaymentPenaltyId = request.PaymentPenaltyId,

                            PaymentDate = request.PaymentDate,
                            PaymentMethodId = request.PaymentMethodId,
                            RequestTypeId = request.RequestTypeId,
                            BankId = request.BankId,
                            ReceiptNumer = request.ReceiptNumer,
                            PaidAmount = request.PaidAmount,

                            PhotoPath = request.PhotoPath,
                            SignaturePath = request.SignaturePath,
                            CreatedBy = UserID,
                            CreatedOn = DateTime.Now
                        };

                        var aadr = Context.Addresses.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                        if (aadr == null)
                        {
                            throw new BusinessRulesException("آدرس متقاضی ثبت نشده است");
                        }
                        cur.ActiveAddressId = aadr.Id;

                        var ajb = Context.Jobs.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                        if (ajb == null)
                        {
                            throw new BusinessRulesException("وظیفه متقاضی ثبت نشده است");
                        }
                        cur.ActiveJobId = ajb.Id;

                        var abd = Context.BioDatas.Where(e => e.ProfileId == request.ProfileId && e.StatusId == 1).SingleOrDefault();
                        if (abd == null)
                        {
                            throw new BusinessRulesException("معلومات شخص تکمیل شده نیست");
                        }
                        cur.ActiveBioDataId = abd.Id;

                        var ex = Context.PassportApplications.Where(e => e.ProfileId == request.ProfileId).ToList();
                        if (ex.Where(e => e.CurProcessId == SystemProcess.Registration).Any())
                        {
                            throw new BusinessRulesException("این شخص یک درخواست ناتکمیل دارد!");
                        }
                        if (ex.Where(e => e.CurProcessId == SystemProcess.Close && e.PassportTypeId == request.PassportTypeId && e.StatusId == ApplicationStatus.Active).Any())
                        {
                            throw new BusinessRulesException("این شخص یک پاسپورت فعال دارد،ابتدا پاسپورت را غیرفعال بسازید!");
                        }

                        var gcode = await GenerateCodeAsync(cur);
                        cur.Code = gcode.Item1;
                        cur.Suffix = gcode.Item2;
                        cur.Prefix = gcode.Item3;

                        var pConfig = await Mediator.Send(new GetApplicationPaymentConfig
                        {
                            PassportTypeID = cur.PassportTypeId,
                            PassportDurationID = cur.PassportDurationId,
                            PaymentCategoryID = cur.PaymentCategoryId,
                            PaymentPenaltyID = cur.PaymentPenaltyId,
                            DiscountID = cur.DiscountId
                        });

                        cur.PaidAmount = pConfig.Amount;

                        cur.CurProcessId = SystemProcess.Registration;
                        Context.PassportApplications.Add(cur);

                        await Context.SaveChangesAsync();
                        
                        ProcessTracking pt = new ProcessTracking
                        {
                            RecordId = cur.Id,
                            ModuleId = SystemModules.Passport,
                            CreatedOn = DateTime.Now,
                            Remarks = "مرحله ثبت متقاضی جریان دارد!",
                            StatusId = ProcessStatus.InProcess,
                            UserId = UserID,
                            ProcessId = SystemProcess.Registration,
                            ReferedProcessId = 0,
                            ToUserId = null
                        };

                        Context.ProcessTracking.Add(pt);

                        await Context.SaveChangesAsync();

                        trans.Commit();

                        return await Mediator.Send(new SearchApplicationQuery { ID = cur.Id });
                    }
                    catch(Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                }
            }
        }

        public async Task<Tuple<string, int?, string>> GenerateCodeAsync(PassportApplication app)
        {
            var officeID = await CurrentUser.GetOfficeID();
            var office = Context.Offices.Where(e => e.Id == officeID).Single();
            StringBuilder PrefixBuilder = new StringBuilder(string.Empty);
            StringBuilder CodeBuilder = new StringBuilder(string.Empty);

            // Build Prefix
            PrefixBuilder.Append(office.Code);
            PrefixBuilder.Append(("00" + DateTime.Now.Month.ToString()).Right(2));
            PrefixBuilder.Append(("00" + DateTime.Now.Day.ToString()).Right(2));
            PrefixBuilder.Append("-");
            PrefixBuilder.Append(("00" + app.PassportTypeId.ToString()).Right(2));
            PrefixBuilder.Append(("00" + app.PassportDurationId.ToString()).Right(2));
            PrefixBuilder.Append(("00" + app.PaymentCategoryId.ToString()).Right(2));
            PrefixBuilder.Append(("00" + app.RequestTypeId.ToString()).Right(2));
            PrefixBuilder.Append("-");

            //Build Suffix
            //Get Current Suffix where its prefix is equal to PrefixBuilder.
            int? Suffix;
            var last = await Context.PassportApplications.Where(p => p.Prefix == PrefixBuilder.ToString()).OrderByDescending(e => e.Suffix).FirstOrDefaultAsync();
            int? CurrentSuffix = last == null ? 0 : last.Suffix;
            if (CurrentSuffix is null) CurrentSuffix = 0;
            Suffix = CurrentSuffix + 1;

            // Build Code
            CodeBuilder.Append(PrefixBuilder.ToString());
            CodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
            return new Tuple<string, int?, string>(CodeBuilder.ToString(), Suffix, PrefixBuilder.ToString());
        }
    }
}
