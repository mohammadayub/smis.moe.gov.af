using Clean.Application.Lookup.Models;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Lookup.Queries
{
    public class GetOrganiztionQuery : IRequest<List<OrganizationModel>>
    {
        public int? Id { get; set; }
    }
    public class GetOrganiztionQueryHandler : IRequestHandler<GetOrganiztionQuery, List<OrganizationModel>>
    {
        private readonly AppIdentityDbContext _dbContext;
        public GetOrganiztionQueryHandler(AppIdentityDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<OrganizationModel>> Handle(GetOrganiztionQuery request, CancellationToken cancellationToken)
        {
            List<OrganizationModel> list = new List<OrganizationModel>();

            var query = _dbContext.Organizations.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }
            
            list = await (from o in query
                          select new OrganizationModel
                          {
                              Id = o.Id,
                              Code = o.Code,
                              Name = o.Name,
                              NameDari = o.Dari,
                              Pashto = o.Pashto,
                              Abbreviation = o.Code,
                              IsActiveText = o.StatusId == 1 ? "فعال" : "غیرفعال"

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
