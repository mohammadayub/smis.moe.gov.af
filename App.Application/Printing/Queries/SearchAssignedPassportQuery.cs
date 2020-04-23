using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Common.Extensions;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Printing.Queries
{
    public class SearchAssignedPassportQuery : IRequest<List<AssignedPassportModel>>
    {
        public long? ID { get; set; }
        public long? PrintQueueID { get; set; }
    }

    public class SearchAssignedPassportQueryHandler : IRequestHandler<SearchAssignedPassportQuery, List<AssignedPassportModel>>
    {
        private int NoPassportAssignedID = -100; 
        private AppDbContext Context { get; }
        private ICurrentUser CurrentUser { get; }
        public SearchAssignedPassportQueryHandler(AppDbContext dbContext,ICurrentUser current)
        {
            Context = dbContext;
            CurrentUser = current;
        }
        public async Task<List<AssignedPassportModel>> Handle(SearchAssignedPassportQuery request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            AssignedPassportModel model = null;
            if(!request.ID.HasValue && !request.PrintQueueID.HasValue)
            {
                return new List<AssignedPassportModel>();
            }
            
            if (request.ID.HasValue)
            {
                if (request.ID == NoPassportAssignedID)
                {
                    var curRecord = Context.PrintQueues.Where(e => e.Id == request.PrintQueueID).Select(e => new { e.Application, e.Application.PassportDuration, e.Application.PassportType }).Single();
                    var app = curRecord.Application;
                    var duration = curRecord.PassportDuration;
                    var ptype = curRecord.PassportType;

                    var stk = Context.StockIns.Where(e => e.ToUserId == UserID
                            && e.StatusId == StockStatus.Active
                            && e.PassportTypeId == app.PassportTypeId
                            && e.PassportDurationId == app.PassportDurationId).SingleOrDefault();

                    if (stk != null)
                    {
                        model = new AssignedPassportModel
                        {
                            ID = NoPassportAssignedID,
                            IssueDateFull = DateTime.Now,
                            ExpiryDateFull = DateTime.Now.AddMonths(duration.Months),
                            IssueDate = DateTime.Now.ToString("dd MMM yyyy"),
                            ExpiryDate = DateTime.Now.AddMonths(duration.Months).ToString("dd MMM yyyy"),
                            IssueDateShamsi = PersianDate.ToPassportFormat(DateTime.Now),
                            ExpiryDateShamsi = PersianDate.ToPassportFormat(DateTime.Now.AddMonths(duration.Months)),
                            PassportNumber = String.Concat(
                                ptype.Code,
                                String.Concat(
                                    Enumerable.Repeat("0", ptype.SerialLength).Aggregate((a, b) => a + b),
                                    (stk.StartSerial + stk.UsedCount)
                                    ).Right(ptype.SerialLength - ptype.Code.Length)),
                            Status = "ثبت نشده",
                            PrintQueueID = request.PrintQueueID.Value
                        };
                    }
                    else
                    {
                        throw new BusinessRulesException("شما در گدام خود این نوع پاسپورت را ندارید!");
                    }
                }
                else
                {
                    var pass = Context.PassportPrints.Where(e => e.Id == request.ID).Single();
                    var passport = Context.Passports.Where(e => e.Id == pass.PassportId).Single();
                    model = new AssignedPassportModel
                    {
                        ID = pass.Id,
                        IssueDateFull = pass.PrintedDate,
                        ExpiryDateFull = pass.ValidTo,
                        IssueDate = pass.PrintedDate.ToString("dd MMM yyyy"),
                        ExpiryDate = pass.ValidTo.ToString("dd MMM yyyy"),
                        IssueDateShamsi = PersianDate.ToPassportFormat(pass.PrintedDate),
                        ExpiryDateShamsi = PersianDate.ToPassportFormat(pass.ValidTo),
                        PassportNumber = passport.PassportNumber,
                        Status = "ثبت شده",
                        PrintQueueID = pass.PrintQueueId
                    };
                }
            }
            else if(request.PrintQueueID.HasValue)
            {
                var curRecord = Context.PrintQueues.Where(e => e.Id == request.PrintQueueID).Select(e => new { e.Application, e.Application.PassportDuration, e.Application.PassportType }).Single();
                var app = curRecord.Application;
                var duration = curRecord.PassportDuration;
                var ptype = curRecord.PassportType;
                var pass = Context.PassportPrints.Where(e => e.PrintQueueId == request.PrintQueueID ).SingleOrDefault();
                if(pass == null)
                {
                    var stk = Context.StockIns.Where(e => e.ToUserId == UserID
                            && e.StatusId == StockStatus.Active
                            && e.PassportTypeId == app.PassportTypeId
                            && e.PassportDurationId == app.PassportDurationId).SingleOrDefault() ;

                    if(stk != null)
                    {
                        model = new AssignedPassportModel
                        {
                            ID = NoPassportAssignedID,
                            IssueDateFull = DateTime.Now,
                            ExpiryDateFull = DateTime.Now.AddMonths(duration.Months),
                            IssueDate = DateTime.Now.ToString("dd MMM yyyy"),
                            ExpiryDate = DateTime.Now.AddMonths(duration.Months).ToString("dd MMM yyyy"),
                            IssueDateShamsi = PersianDate.ToPassportFormat( DateTime.Now),
                            ExpiryDateShamsi = PersianDate.ToPassportFormat( DateTime.Now.AddMonths(duration.Months)),
                            PassportNumber = String.Concat(
                                ptype.Code,
                                String.Concat(
                                    Enumerable.Repeat("0", ptype.SerialLength).Aggregate((a, b) => a + b),
                                    (stk.StartSerial + stk.UsedCount)
                                    ).Right(ptype.SerialLength - ptype.Code.Length)),
                            Status = "ثبت نشده",
                            PrintQueueID = request.PrintQueueID.Value
                        };
                    }
                    else
                    {
                        throw new BusinessRulesException("شما در گدام خود این نوع پاسپورت را ندارید!");
                    }
                }
                else
                {
                    var passport = Context.Passports.Where(e => e.Id == pass.PassportId).Single();
                    model = new AssignedPassportModel
                    {
                        ID = pass.Id,
                        IssueDateFull = pass.PrintedDate,
                        ExpiryDateFull = pass.ValidTo,
                        IssueDate = pass.PrintedDate.ToString("dd MMM yyyy"),
                        ExpiryDate = pass.ValidTo.ToString("dd MMM yyyy"),
                        IssueDateShamsi = PersianDate.ToPassportFormat(pass.PrintedDate),
                        ExpiryDateShamsi = PersianDate.ToPassportFormat(pass.ValidTo),
                        PassportNumber = passport.PassportNumber,
                        Status = "ثبت شده",
                        PrintQueueID = pass.PrintQueueId
                    };
                }
            }
            if(model == null)
            {
                return new List<AssignedPassportModel>();
            }
            else
            {

                return new List<AssignedPassportModel> { model };
            }
        }
    }
}
