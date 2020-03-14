using Clean.Application.ProcessTracking.Models;
using Clean.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.ProcessTracking.Queries
{
    public class GetProcessConnection : IRequest<List<SearchedProcessConnection>>
    {
        public int? Id { get; set; }
        public int? ModuleId { get; set; }
        public int? ScreenId { get; set; }
        public int? ProcessID { get; set; }

    }
    public class GetProcessConnectionHandler : IRequestHandler<GetProcessConnection, List<SearchedProcessConnection>>
    {
        private BaseContext _context;
        public GetProcessConnectionHandler(BaseContext context)
        {
            _context = context;
        }
        public async Task<List<SearchedProcessConnection>> Handle(GetProcessConnection request, CancellationToken cancellationToken)
        {


            List<SearchedProcessConnection> result = new List<SearchedProcessConnection>();
            result = await _context.ProcessConnection
                .Include(e => e.Process)
                    .ThenInclude(e => e.Screen)
                .Include(e => e.ConnectedToNavigation)
                .Select(e => new SearchedProcessConnection
                {
                    Id = e.Id,
                    ProcessId = e.ProcessId,
                    ConnectionId = e.ConnectedTo,
                    ProcessText = e.Process.Name,
                    ConnectionText = e.ConnectedToNavigation.Name,
                    ScreenId = e.Process.Screen.Id,
                    ModuleId = e.Process.Screen.ModuleId.Value,
                    Sorter = _context.Process.Where(d => d.Id == e.ConnectedTo).SingleOrDefault().Sorter
                }).OrderBy(e => e.Sorter).ToListAsync();


            return result;


        }
    }
}
