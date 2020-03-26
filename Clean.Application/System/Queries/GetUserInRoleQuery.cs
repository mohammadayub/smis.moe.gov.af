using Clean.Application.System.Models;
using Clean.Persistence.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.System.Queries
{
    public class GetUserInRoleQuery : IRequest<List<SearchedUserInRoleModel>>
    {
        public int? Id { get; set; }
        public int? UserID { get; set; }
        public int RoleId { get; set; }
    }
    public class GetUserInRoleQueryHandler : IRequestHandler<GetUserInRoleQuery, List<SearchedUserInRoleModel>>
    {
        private readonly AppIdentityDbContext _dbContext;

        public GetUserInRoleQueryHandler(AppIdentityDbContext identityContext)
        {
            _dbContext = identityContext;

        }
        public async Task<List<SearchedUserInRoleModel>> Handle(GetUserInRoleQuery request, CancellationToken cancellationToken)
        {
            List<SearchedUserInRoleModel> fresult = new List<SearchedUserInRoleModel>();

            if (request.UserID.HasValue)
            {
                fresult = await (from r in _dbContext.UserRoles
                                 join u in _dbContext.Users on r.UserId equals u.Id
                                 join role in _dbContext.Roles on r.RoleId equals role.Id
                                 where r.UserId == request.UserID
                                 select new SearchedUserInRoleModel
                                 {
                                     Id = String.Concat( r.UserId , "_" , r.RoleId),
                                     UserId = r.UserId,
                                     RoleId = r.RoleId,
                                     UserName = u.UserName,
                                     RoleName = role.Name
                                 }).ToListAsync(cancellationToken);

            }
            else
            {
                fresult = await (from r in _dbContext.UserRoles
                                 join u in _dbContext.Users on r.UserId equals u.Id
                                 join role in _dbContext.Roles on r.RoleId equals role.Id
                                 where r.RoleId == request.RoleId
                                 select new SearchedUserInRoleModel
                                 {
                                     Id = String.Concat( r.UserId , "_" , r.RoleId),
                                     UserId = r.UserId,
                                     RoleId = r.RoleId,
                                     UserName = u.UserName,
                                     RoleName = role.Name
                                 }).ToListAsync(cancellationToken);

            }


            return fresult;
        }
    }
}
