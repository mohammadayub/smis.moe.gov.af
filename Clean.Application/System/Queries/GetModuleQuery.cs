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
    public class GetModuleQuery : IRequest<List<Module>>
    {
        public int? ID { get; set; }
    }
    public class GetModulepQueryHandler : IRequestHandler<GetModuleQuery, List<Module>>
    {
        private readonly AppIdentityDbContext _dbContext;
        public GetModulepQueryHandler(AppIdentityDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Module>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
        {


            if (request.ID.HasValue)
                return await _dbContext.Modules.Where(m => m.Id == request.ID).ToListAsync();
            else
                return await _dbContext.Modules.ToListAsync();
        }
    }
}
