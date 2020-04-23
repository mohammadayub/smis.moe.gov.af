using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Application.ProcessTrackings.Commands;
using Clean.Application.ProcessTrackings.Models;
using Clean.Common.Enums;
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
    public class ProcessPrintedPassportCommand : IRequest<PassportPrintProcessModel>
    {
        public long ID { get; set; }
    }

    public class ProcessPrintedPassportCommandHandler : IRequestHandler<ProcessPrintedPassportCommand, PassportPrintProcessModel>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        private IMediator Mediator { get; set; }
        public ProcessPrintedPassportCommandHandler(AppDbContext dbContext,ICurrentUser current,IMediator mediator)
        {
            Context = dbContext;
            CurrentUser = current;
            Mediator = mediator;
        }
        public async Task<PassportPrintProcessModel> Handle(ProcessPrintedPassportCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            PassportPrintProcessModel model = new PassportPrintProcessModel { Processed = false,Marked = false };
            try
            {
                var pp = Context.PassportPrints.Where(e => e.Id == request.ID).SingleOrDefault();
                if(pp == null)
                {
                    model.ProcessError = "پرنت مربوطه پیدا نشد!";
                }
                else
                {
                    try
                    {
                        pp.StatusId = PassportPrintStatus.Printed;
                        await Context.SaveChangesAsync();
                        model.Marked = true;

                        var app = Context.PrintQueues.Where(e => e.Id == pp.PrintQueueId).Select(e => e.Application).Single();
                        var prcs = await Mediator.Send(new SearchProcessTrackQuery() { RecordId = app.Id, ModuleId = SystemModules.Passport });
                        var curPrc = prcs.FirstOrDefault();
                        if (curPrc == null)
                        {
                            model.ProcessError = "پاسپورت پروسس نشده است!";
                        }
                        else
                        {
                            var rs = await Mediator.Send(new SaveProcessTracksCommand
                            {
                                Id = curPrc.Id,
                                RecordId = curPrc.RecordId,
                                ProcessId = curPrc.ProcessId,
                                ReferedProcessId = SystemProcess.QualityControl,
                                ModuleId = SystemModules.Passport
                            });
                            pp.StatusId = PassportPrintStatus.Printed;
                            await Context.SaveChangesAsync();
                            model.Processed = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Marked = false;
                        model.MarkError = ex.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                model.ProcessError = ex.Message;
            }
            return model;
        }
    }
}
