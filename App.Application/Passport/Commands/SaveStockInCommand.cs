using App.Application.Passport.Models;
using App.Application.Passport.Queries;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Passport.Commands
{
    public class SaveStockInCommand : IRequest<List<StockInModel>>
    {
        public int? Id { get; set; }
        public int PassportTypeId { get; set; }
        public int PassportDurationId { get; set; }
        public int StartSerial { get; set; }
        public int EndSerial { get; set; }
        public int PassportCount { get; set; }
        public int StatusId { get; set; }
        public int? ToUserId { get; set; }
    }

    public class SaveStockInCommandHandler : IRequestHandler<SaveStockInCommand, List<StockInModel>>
    {
        private AppDbContext Context { get; }
        private ICurrentUser CurrectUser { get; }
        private IMediator Mediator { get; }
        public SaveStockInCommandHandler(AppDbContext dbContext,ICurrentUser current,IMediator mediator)
        {
            Context = dbContext;
            CurrectUser = current;
            Mediator = mediator;
        }
        public async Task<List<StockInModel>> Handle(SaveStockInCommand request, CancellationToken cancellationToken)
        {
            if (request.StatusId == StockStatus.Completed)
            {
                throw new BusinessRulesException("شما نمی توانید ریکارد را تکمیل شده تعیین کنید!");
            }
            if(request.StartSerial > request.EndSerial)
            {
                throw new BusinessRulesException("نمبر شروع باید کوچکتر از نمبر ختم باشد!");
            }
            var CUserID = await CurrectUser.GetUserId();
            var UserID = request.ToUserId.HasValue ? request.ToUserId.Value : CUserID;
            if (request.Id.HasValue)
            {
                var cur = Context.StockIns.Where(e => e.Id == request.Id).Single();
                if(cur.StatusId == StockStatus.Completed)
                {
                    throw new BusinessRulesException("این ریکارد قابل تغییر نمی باشد!");
                }
                var StockPassports = await Context.Passports.Where(e => e.StockInId == cur.Id).OrderBy(e => e.SerialNumber).ToListAsync();
                var first = StockPassports.FirstOrDefault();
                if (first != null && request.StartSerial > first.SerialNumber)
                {
                    throw new BusinessRulesException("شماره‌های قبلی پرنت شده‌اند!");
                }
                var last = StockPassports.LastOrDefault();
                if(last != null && request.EndSerial < last.SerialNumber)
                {
                    throw new BusinessRulesException("شماره‌های بعدی پرنت شده‌اند!");
                }

                if(cur.PassportTypeId != request.PassportTypeId)
                {
                    throw new BusinessRulesException("نوعیت پاسپورت قابل تغییر نمی باشد!");
                }

                if (Context.StockIns.Where(e => e.Id != request.Id && ((request.StartSerial >= e.StartSerial && request.StartSerial <= e.EndSerial) || (request.EndSerial >= e.StartSerial && request.EndSerial <= e.EndSerial))).Any())
                {
                    throw new BusinessRulesException("این سریال نمبر قبلا در سیستم موجود است!");
                }

                if (request.StatusId == StockStatus.Active)
                {
                    if (Context.StockIns.Where(e => e.ToUserId == UserID && e.StatusId == StockStatus.Active && e.Id != request.Id && e.PassportTypeId == request.PassportTypeId).Any())
                    {
                        throw new BusinessRulesException("پاسپورت‌های فعال این کاربر تکمیل نشده است!");
                    }
                }

                cur.PassportTypeId = request.PassportTypeId;
                cur.PassportDurationId = request.PassportDurationId;

                cur.StartSerial = request.StartSerial;
                cur.EndSerial = request.EndSerial;
                cur.PassportCount = request.EndSerial - request.StartSerial + 2;
                cur.ToUserId = UserID;
                cur.StatusId = request.StatusId;

                cur.ModifiedBy = CUserID;
                cur.ModifiedOn = DateTime.Now;

                

                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchStockInQuery { ID = cur.Id });
            }
            else
            {
                var stk = new Domain.Entity.stc.StockIn
                {
                    PassportTypeId = request.PassportTypeId,
                    PassportDurationId = request.PassportDurationId,
                    StartSerial = request.StartSerial,
                    EndSerial = request.EndSerial,
                    PassportCount = request.EndSerial - request.StartSerial + 2,
                    UsedCount = 0,
                    ToUserId = UserID,
                    StatusId = request.StatusId,
                    CreatedBy = CUserID,
                    CreatedOn = DateTime.Now 
                };

                if(Context.StockIns.Where(e => ( stk.StartSerial >= e.StartSerial && stk.StartSerial <= e.EndSerial) || (stk.EndSerial >= e.StartSerial && stk.EndSerial <= e.EndSerial)).Any())
                {
                    throw new BusinessRulesException("این سریال نمبر قبلا در سیستم موجود است!");
                }

                if(stk.StatusId == StockStatus.Active)
                {
                    if(Context.StockIns.Where(e => e.ToUserId == UserID && e.StatusId == StockStatus.Active && e.PassportTypeId == stk.PassportTypeId).Any())
                    {
                        throw new BusinessRulesException("پاسپورت‌های فعال این کاربر تکمیل نشده است!");
                    }
                }

                Context.StockIns.Add(stk);
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchStockInQuery { ID = stk.Id });
            }
        }
    }
}
