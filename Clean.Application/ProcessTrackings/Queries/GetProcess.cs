using Clean.Application.ProcessTrackings.Models;
using Clean.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.ProcessTrackings.Queries
{
    public class GetProcess : IRequest<List<SearchedProcess>>
    {
        public int? Id { get; set; }
        public int? ModuleId { get; set; }
        public int? ScreenId { get; set; }
        public bool? GetAllProcessForUser { get; set; }
    }
    public class GetProcessHandler : IRequestHandler<GetProcess, List<SearchedProcess>>
    {
        private BaseContext _context;
        public GetProcessHandler(BaseContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedProcess>> Handle(GetProcess request, CancellationToken cancellationToken)
        {
            List<SearchedProcess> result = new List<SearchedProcess>();
            if (request.ScreenId != null)
            {
                result = await (from P in _context.Process
                                join S in _context.Screens on P.ScreenId equals S.Id into Screens
                                from ScreenResults in Screens.DefaultIfEmpty()
                                join M in _context.Modules on ScreenResults.ModuleId equals M.Id into Modules
                                from ModuleResult in Modules.DefaultIfEmpty()
                                where P.ScreenId == request.ScreenId
                                select new SearchedProcess
                                {
                                    Id = P.Id,
                                    Name = P.Name,
                                    Description = P.Description,
                                    ScreenId = P.ScreenId,
                                    ModuleId = ModuleResult.Id,

                                }).ToListAsync(cancellationToken);
            }
            else if (request.GetAllProcessForUser == true)
            {
                result = await _context.Process.Select(e => new SearchedProcess
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Sorter = e.Sorter
                }).OrderBy(e => e.Sorter).ToListAsync();
            }
            return result;
        }
    }
}
