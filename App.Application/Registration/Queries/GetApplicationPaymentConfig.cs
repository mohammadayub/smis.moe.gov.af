using App.Application.Registration.Models;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Queries
{
    public class GetApplicationPaymentConfig:IRequest<AppPaymentConfigResult>
    {
        public int PassportTypeID { get; set; }
        public int PassportDurationID { get; set; }
        public int PaymentCategoryID { get; set; }
        public int? DiscountID { get; set; }
        public int? PaymentPenaltyID { get; set; }
        public int? ApplicationID { get; set; }
    }

    public class GetApplicationPaymentConfigHandler : IRequestHandler<GetApplicationPaymentConfig, AppPaymentConfigResult>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        public GetApplicationPaymentConfigHandler(AppDbContext context,ICurrentUser current)
        {
            Context = context;
            CurrentUser = current;
        }
        public async Task<AppPaymentConfigResult> Handle(GetApplicationPaymentConfig request, CancellationToken cancellationToken)
        {
            var config = new AppPaymentConfigResult();
            if (request.ApplicationID.HasValue)
            {
                var cur = await Context.PassportApplications.Where(e => e.Id == request.ApplicationID).SingleAsync();
                if(cur.CurProcessId != SystemProcess.Registration)
                {
                    config.Exists = true;
                    config.Amount = cur.PaidAmount;
                }
                else
                {
                    config = await CalculatePaymentAmount(request);
                }
            }
            else
            {
                config = await CalculatePaymentAmount(request);
            }
            return config;
        }

        private async Task<AppPaymentConfigResult> CalculatePaymentAmount(GetApplicationPaymentConfig request)
        {
            var cfg = new AppPaymentConfigResult();
            var OfficeID = await CurrentUser.GetOfficeID();
            var config = await Context.PaymentConfigs
                .Where(e => e.PassportTypeId == request.PassportTypeID
                && e.PassportDurationId == request.PassportDurationID
                && e.PaymentCategoryId == request.PaymentCategoryID
                && e.OfficeId == OfficeID
                && e.StatusId == 1).SingleOrDefaultAsync();

            if(config != null)
            {
                cfg.Exists = true;
                cfg.Amount = config.Amount;
                cfg.HasPenalty = false;
                if (request.PaymentPenaltyID.HasValue)
                {
                    var pent = await Context.PaymentPenalties.Where(e => e.Id == request.PaymentPenaltyID).SingleAsync();
                    cfg.Penalty = pent.Amount;
                    cfg.HasPenalty = true;
                }
                cfg.HasDiscount = false;
                if (request.DiscountID.HasValue)
                {
                    var disc = await Context.Discounts.Where(e => e.Id == request.DiscountID).Select(e => new {e.Id,e.Amount,e.DiscountType.Code }).SingleAsync();
                    cfg.Discount = disc.Amount;
                    cfg.DiscountType = disc.Code;
                    cfg.HasDiscount = true;
                }
                if (cfg.HasDiscount)
                {
                    if(cfg.DiscountType == DiscountTypes.Percentage)
                    {
                        cfg.Amount -= cfg.Amount * cfg.Discount / 100;
                    }
                    else if(cfg.DiscountType == DiscountTypes.WholePrice)
                    {
                        cfg.Amount = cfg.Discount;
                    }
                    else if(cfg.DiscountType == DiscountTypes.DiscountPrice)
                    {
                        cfg.Amount -= cfg.Discount;
                    }
                }
                if (cfg.HasPenalty)
                {
                    cfg.Amount += cfg.Penalty;
                }
            }
            else
            {
                cfg.Exists = false;
                cfg.Amount = 0;
            }
            return cfg;
        }
    }
}
