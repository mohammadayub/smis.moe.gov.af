using Clean.Domain.Entity.look;
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

namespace Clean.Application.System.Queries
{
    public class GetScreens : IRequest<List<Screen>>
    {
        public int? ID { get; set; }
        public int? ModuleID { get; set; }
        public int? ModuleIDNotIn { get; set; }
        public int? ParentId { get; set; }

    }
    public class GetScreensHandler : IRequestHandler<GetScreens, List<Screen>>
    {
        private AppIdentityDbContext _context;
        public GetScreensHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Screen>> Handle(GetScreens request, CancellationToken cancellationToken)
        {

            if (request.ModuleID.HasValue)
            {

                return await _context.Screens.Where(c => c.ParentId == null && c.ModuleId == request.ModuleID).OrderBy(c => c.Sorter).ToListAsync(cancellationToken);
            }
            else if (request.ModuleIDNotIn != null)
            {
                return await _context.Screens.Where(s => s.ModuleId != request.ModuleIDNotIn).OrderBy(c => c.Sorter).ToListAsync(cancellationToken);
            }
            else if (request.ID == null && request.ModuleID == null && request.ModuleIDNotIn == null)
            {
                // Return all screens. This case is mostly used for SuperAdmin
                return await _context.Screens.ToListAsync(cancellationToken);
            }
            else
            {
                return await _context.Screens.Where(s => s.Id == request.ID).ToListAsync(cancellationToken);
            }



        }
    }
}
