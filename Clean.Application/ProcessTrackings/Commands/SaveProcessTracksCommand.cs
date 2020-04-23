using App.Persistence.Service;
using Clean.Application.ProcessTrackings.Models;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Domain.Entity.prc;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.ProcessTrackings.Commands
{
    public class SaveProcessTracksCommand : IRequest<List<SearchedProcessTracks>>
    {
        public long? Id { get; set; }
        public long RecordId { get; set; }
        public int? ProcessId { get; set; }
        public int ReferedProcessId { get; set; }
        public short StatusId { get; set; }
        public string Remarks { get; set; }
        public int? ModuleId { get; set; }
        public int UserId { get; set; }
        public string LogicId { get; set; } // pre ckeck up id
        public int? CategoryId { get; set; }
    }


    public class SaveProcessTracksCommandHandler : IRequestHandler<SaveProcessTracksCommand, List<SearchedProcessTracks>>
    {
        private BaseContext Context { get; }
        private IMediator Mediator { get; }
        private ICurrentUser CurrentUser { get; }
        private IEnumerable<IProcessChangeListener> ChangeListeners { get; set; }

        public SaveProcessTracksCommandHandler(BaseContext context, IMediator mediator,ICurrentUser  current,IEnumerable<IProcessChangeListener> listeners)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = current;
            ChangeListeners = listeners;
        }

        public async Task<List<SearchedProcessTracks>> Handle(SaveProcessTracksCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            List<SearchedProcessTracks> result = new List<SearchedProcessTracks>();
            if (request.Id == null)
            {

                ProcessTracking PT = new ProcessTracking()
                {
                    RecordId = request.RecordId,
                    ProcessId = request.ProcessId.Value,
                    StatusId = ProcessStatus.InProcess,
                    ModuleId = request.ModuleId.Value,
                    ReferedProcessId = request.ReferedProcessId,
                    CreatedOn = DateTime.Now,
                    UserId = UserID,
                    ToUserId = null
                };
                Context.ProcessTracking.Add(PT);
                await Context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                using (var Transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        int? ToUserId = null;
                        // Update current process track status
                        ProcessTracking track = await Context.ProcessTracking.Where(pt => pt.Id == request.Id).SingleOrDefaultAsync();
                        track.ReferedProcessId = request.ReferedProcessId;

                        // find the refered and previous process id sorter to check for approve and reject
                        int ProcessSorter = Convert.ToInt32(Context.Process.Where(e => e.Id == track.ProcessId).SingleOrDefault().Sorter);
                        int ReferedProcessSorter = Convert.ToInt32(Context.Process.Where(e => e.Id == request.ReferedProcessId).SingleOrDefault().Sorter);

                        track.StatusId = (ReferedProcessSorter < ProcessSorter) ? ProcessStatus.Rejected : ProcessStatus.Processed; // 102 rad shuda. 4 Tayeed shuda. Check se.Status

                        // find next or previouse ModuleId from referedProcessId
                        int? ModuleID;
                        if (request.ReferedProcessId != 0)
                        {
                            int? screedID = Context.Process.Where(e => e.Id == request.ReferedProcessId).SingleOrDefault().ScreenId;

                            if (screedID == null && request.ReferedProcessId == SystemProcess.Close)
                                ModuleID = (int)SystemModules.Passport;
                            else
                                ModuleID = Context.Screens.Where(e => e.Id == screedID).SingleOrDefault().ModuleId ;
                        }
                        else
                        {
                            ModuleID = request.ModuleId;
                        }
                        // Add new process track
                        int ProcessStatusId = ProcessStatus.InProcess;
                        //check if process is the final one change process status to processed
                        if (request.ReferedProcessId == SystemProcess.Close)
                            ProcessStatusId = (int)ProcessStatus.Processed;



                        ProcessTracking PT = new ProcessTracking()
                        {
                            RecordId = request.RecordId,
                            ProcessId = (Int16)request.ReferedProcessId,
                            StatusId = ProcessStatusId,
                            Remarks = request.Remarks,
                            ModuleId = ModuleID.Value,
                            CreatedOn = DateTime.Now,
                            UserId = UserID,
                            ToUserId = ToUserId
                        };
                        Context.ProcessTracking.Add(PT);
                        await Context.SaveChangesAsync(cancellationToken);

                        foreach (var item in ChangeListeners.Where(e => e.ModuleID == ModuleID))
                        {
                            await item.ProcessChangedAsync(request.RecordId,track.ReferedProcessId,track.ProcessId, Context);
                        } 

                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        throw new InternalSystemException(ex.Message) { MessageDetails = ex.Message, StackTraceDetails = ex.StackTrace, InnerExceptionDetails = ex.InnerException };
                    }
                }
            }
            result = await Mediator.Send(new SearchProcessTrackQuery()
            {
                RecordId = request.RecordId,
                ModuleId = request.ModuleId
            });
            return result;
        }


    }
}
