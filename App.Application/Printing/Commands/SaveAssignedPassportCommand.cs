using App.Application.Printing.Models;
using App.Application.Printing.Queries;
using App.Domain.Entity.prt;
using App.Domain.Entity.stc;
using App.Persistence.Context;
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

namespace App.Application.Printing.Commands
{
    public class SaveAssignedPassportCommand : IRequest<List<AssignedPassportModel>>
    {
        public long ID { get; set; }
        public int PrintQueueID { get; set; }
    }

    public class SaveAssignedPassportCommandHandler : IRequestHandler<SaveAssignedPassportCommand, List<AssignedPassportModel>>
    {
        private int NoPassportAssignedID = -100;
        private AppDbContext Context { get; }
        private ICurrentUser CurrentUser { get; }
        private IMediator Mediator { get; }
        public SaveAssignedPassportCommandHandler(AppDbContext dbContext,ICurrentUser current,IMediator mediator)
        {
            Context = dbContext;
            CurrentUser = current;
            Mediator = mediator;
        }
        public async Task<List<AssignedPassportModel>> Handle(SaveAssignedPassportCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            if(request.ID == NoPassportAssignedID)
            {
                var printQueue = Context.PrintQueues.Where(e => e.Id == request.PrintQueueID).Single();
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
                    var pass = new Passports
                    {
                        StockInId = stk.Id,
                        SerialNumber = stk.StartSerial + stk.UsedCount,
                        PassportNumber = String.Concat(
                                ptype.Code,
                                String.Concat(
                                    Enumerable.Repeat("0", ptype.SerialLength).Aggregate((a, b) => a + b),
                                    (stk.StartSerial + stk.UsedCount)
                                    ).Right(ptype.SerialLength - ptype.Code.Length)),
                        StatusId = PassportStatus.Active,
                        UserId = UserID,
                        CreatedBy = UserID,
                        CreatedOn = DateTime.Now
                    };
                    var print = new PassportPrint
                    {
                        Passport = pass,
                        PrintedDate = DateTime.Now,
                        ValidTo = DateTime.Now.AddMonths(duration.Months),
                        PrintQueueId = request.PrintQueueID,
                        StatusId = PassportPrintStatus.Registered,
                        CreatedBy = UserID,
                        CreatedOn = DateTime.Now,
                    };

                    if(stk.EndSerial == pass.SerialNumber)
                    {
                        stk.StatusId = StockStatus.Completed;
                    };

                    stk.UsedCount++;

                    Context.PassportPrints.Add(print);
                    await Context.SaveChangesAsync();

                    return await Mediator.Send(new SearchAssignedPassportQuery { ID = print.Id, PrintQueueID = request.PrintQueueID });
                }
                else
                {
                    throw new BusinessRulesException("شما در گدام خود این نوع پاسپورت را ندارید!");
                }
            }
            else
            {
                return await Mediator.Send(new SearchAssignedPassportQuery { ID = request.ID, PrintQueueID = request.PrintQueueID });
            }
        }
    }
}
