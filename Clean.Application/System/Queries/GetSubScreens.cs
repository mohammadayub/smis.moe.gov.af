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
    public class GetSubScreens : IRequest<List<Screen>>
    {
        public int ID { get; set; }
    }

    public class GetScreensByParentIDHandler : IRequestHandler<GetSubScreens, List<Screen>>
    {
        private AppIdentityDbContext _context;
        public GetScreensByParentIDHandler(AppIdentityDbContext context)
        {
            _context = context;
        }


        public async Task<List<Screen>> Handle(GetSubScreens request, CancellationToken cancellationToken)
        {
            return await _context.Screens.Where(s => s.ParentId == request.ID).OrderBy(c => c.Sorter).ToListAsync(cancellationToken);
        }
    }
}
