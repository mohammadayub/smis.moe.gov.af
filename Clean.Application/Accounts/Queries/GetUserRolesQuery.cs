using Clean.Application.Accounts.Models;
using Clean.Persistence.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Accounts.Queries
{
    public class GetUserRolesQuery : IRequest<List<SearchedUserInRoleModel>>
    {
        public int? UserID { get; set; }
        public int? RoleId { get; set; }
    }
    public class GetUserInRoleQueryHandler : IRequestHandler<GetUserRolesQuery, List<SearchedUserInRoleModel>>
    {
        private readonly AppIdentityDbContext _dbContext;

        public GetUserInRoleQueryHandler(AppIdentityDbContext identityContext)
        {
            _dbContext = identityContext;

        }
        public async Task<List<SearchedUserInRoleModel>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            List<SearchedUserInRoleModel> fresult = new List<SearchedUserInRoleModel>();
            var query = _dbContext.UserRoles
                .Include(e => e.Role)
                .Include(e => e.User)
                .AsQueryable();

            if (request.UserID.HasValue)
            {
                query = query.Where(e => e.UserId == request.UserID);

            }
            else
            {
                query = query.Where(e => e.RoleId == request.RoleId);
            }

            fresult = query.Select(e => new SearchedUserInRoleModel
            {
                Id = String.Concat( e.UserId , "_" , e.RoleId),
                UserId = e.UserId,
                RoleId = e.RoleId,
                UserName = e.User.UserName,
                RoleName = e.Role.Name
            }).ToList();


            return fresult;
        }
    }
}
