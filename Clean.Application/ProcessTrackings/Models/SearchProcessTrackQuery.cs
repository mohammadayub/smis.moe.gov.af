using Clean.Persistence.Context;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Clean.Common.Dates;
using Microsoft.EntityFrameworkCore;
using Clean.Common.Enums;

namespace Clean.Application.ProcessTrackings.Models
{
    
    public class SearchProcessTrackQuery : IRequest<List<SearchedProcessTracks>>
    {
        public long RecordId { get; set; }
        public int? ModuleId { get; set; }
    }


    public class SearchProcessTrackQueryHandler : IRequestHandler<SearchProcessTrackQuery, List<SearchedProcessTracks>>
        {
            private BaseContext _context;
            UserManager<AppUser> _userManager;
            public SearchProcessTrackQueryHandler(BaseContext context, UserManager<AppUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }
            public async Task<List<SearchedProcessTracks>> Handle(SearchProcessTrackQuery request, CancellationToken cancellationToken)
            {
                List<SearchedProcessTracks> result = new List<SearchedProcessTracks>();
                result = await (from PT in _context.ProcessTracking
                                join PR in _context.Process on PT.ProcessId equals PR.Id into Processes
                                from ProcessResult in Processes.DefaultIfEmpty()
                                join M in _context.Modules on PT.ModuleId equals M.Id into Modules
                                from ModuleResult in Modules.DefaultIfEmpty()
                                join S in _context.SystemStatus on new { TypeId = PT.StatusId, StatusType = StatusTypes.ProcessTracking } equals new { S.TypeId ,S.StatusType } into Status
                                from StatusResult in Status.DefaultIfEmpty()
                                where PT.RecordId == request.RecordId && PT.ModuleId == request.ModuleId
                                select new SearchedProcessTracks
                                {
                                    Id = PT.Id,
                                    RecordId = PT.RecordId,
                                    ModuleId = PT.ModuleId,
                                    ProcessId = PT.ProcessId,
                                    StatusId = PT.StatusId,
                                    Remarks = PT.Remarks,
                                    ReferedProcessId = PT.ReferedProcessId,
                                    ProcessText = ProcessResult.Name,
                                    ModuleText = ModuleResult.Name,
                                    CreatedOn = PT.CreatedOn,
                                    StatusText = StatusResult.Title,
                                    DateText = PersianDate.GetFormatedString(PT.CreatedOn),
                                    UserId = PT.UserId,
                                    UserName = _userManager.FindByIdAsync(PT.UserId.ToString()).Result.FirstName,
                                    TimeText = PT.CreatedOn.ToString("h:mm tt"),
                                    ToUserId = PT.ToUserId,
                                    ToUserName = PT.ToUserId != null ? _userManager.FindByIdAsync(PT.ToUserId.ToString()).Result.FirstName : ""
                                }).OrderByDescending(c => c.CreatedOn).ToListAsync(cancellationToken);
                return result;
            }
        }
    
}
