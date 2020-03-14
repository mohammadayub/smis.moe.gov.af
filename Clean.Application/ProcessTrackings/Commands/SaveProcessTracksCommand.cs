using Clean.Application.ProcessTrackings.Models;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Domain.Entity.prc;
using Clean.Persistence.Context;
using Clean.Persistence.Identity;
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
        public int? Id { get; set; }
        public int RecordId { get; set; }
        public int? ProcessId { get; set; }
        public short ReferedProcessId { get; set; }
        public short StatusId { get; set; }
        public string Remarks { get; set; }
        public int? ModuleId { get; set; }
        public int UserId { get; set; }
        public string LogicId { get; set; } // pre ckeck up id
        public int? CategoryId { get; set; }
    }


    public class SaveProcessTracksCommandHandler : IRequestHandler<SaveProcessTracksCommand, List<SearchedProcessTracks>>
    {
        private readonly BaseContext _context;
        private readonly AppIdentityDbContext IdentityContext;
        private readonly IMediator _mediator;
        public SaveProcessTracksCommandHandler(BaseContext context, AppIdentityDbContext identityDbContext, IMediator mediator)
        {
            _context = context;
            IdentityContext = identityDbContext;
            _mediator = mediator;
        }

        public async Task<List<SearchedProcessTracks>> Handle(SaveProcessTracksCommand request, CancellationToken cancellationToken)
        {
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
                    UserId = request.UserId,
                    ToUserId = null
                };
                _context.ProcessTracking.Add(PT);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                using (var Transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        int? ToUserId = null;
                        // Update current process track status
                        ProcessTracking track = await _context.ProcessTracking.Where(pt => pt.Id == request.Id).SingleOrDefaultAsync();
                        track.ReferedProcessId = request.ReferedProcessId;

                        // find the refered and previous process id sorter to check for approve and reject
                        string ProcessSorter = _context.Process.Where(e => e.Id == track.ProcessId).SingleOrDefault().Sorter;
                        string ReferedProcessSorter = _context.Process.Where(e => e.Id == request.ReferedProcessId).SingleOrDefault().Sorter;

                        track.StatusId = (ReferedProcessSorter.CompareTo(ProcessSorter) > 1) ? ProcessStatus.Rejected : ProcessStatus.Processed; // 102 rad shuda. 4 Tayeed shuda. Check se.Status

                        // find next or previouse ModuleId from referedProcessId
                        int? ModuleID;
                        if (request.ReferedProcessId != 0)
                        {
                            int? screedID = _context.Process.Where(e => e.Id == request.ReferedProcessId).SingleOrDefault().ScreenId;

                            if (screedID == null && request.ReferedProcessId == SystemProcess.Close)
                                ModuleID = (int)SystemModules.Payment;
                            else
                                ModuleID = _context.Screens.Where(e => e.Id == screedID).SingleOrDefault().ModuleId ;
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
                            UserId = request.UserId,
                            ToUserId = ToUserId
                        };
                        _context.ProcessTracking.Add(PT);
                        await _context.SaveChangesAsync(cancellationToken);

                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                        throw new InternalSystemException(ex.Message) { MessageDetails = ex.Message, StackTraceDetails = ex.StackTrace, InnerExceptionDetails = ex.InnerException };
                    }
                }
            }
            result = await _mediator.Send(new SearchProcessTrackQuery()
            {
                RecordId = request.RecordId,
                ModuleId = request.ModuleId
            });
            return result;
        }


    }
}
