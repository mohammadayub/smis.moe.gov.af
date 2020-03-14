using Clean.Application.System.Models;
using Clean.Persistence.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.System.Queries
{
    public class GetRoleQuery : IRequest<List<SearchedRoleModel>>
    {

        public int? Id { get; set; }
        public string RoleName { get; set; }

    }
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<SearchedRoleModel>>
    {
        private readonly AppIdentityDbContext _dbContext;
        public GetRoleQueryHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SearchedRoleModel>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            List<SearchedRoleModel> result = new List<SearchedRoleModel>();
            List<AppUser> LisOfUsers = new List<AppUser>();



            if (request.Id != null)
            {
                AppRole role = await (from r in _dbContext.Roles where r.Id == request.Id select r).SingleOrDefaultAsync();
                result.Add(new SearchedRoleModel() { Id = role.Id, RoleName = role.Name });
            }


            else if (!string.IsNullOrEmpty(request.RoleName))
            {
                List<AppRole> roles = await (from r in _dbContext.Roles where EF.Functions.Like(r.Name, String.Concat("%", request.RoleName, "%")) select r).ToListAsync();

                foreach (AppRole role in roles)
                    result.Add(new SearchedRoleModel() { Id = role.Id, RoleName = role.Name });
            }

            else
            {
                foreach (AppRole role in (await (from r in _dbContext.Roles select r).Take(100).ToListAsync()))
                    result.Add(new SearchedRoleModel() { Id = role.Id, RoleName = role.Name });
            }


            return result;
        }
    }
}
