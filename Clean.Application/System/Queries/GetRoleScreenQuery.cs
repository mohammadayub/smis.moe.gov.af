using Clean.Application.System.Models;
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
    public class GetRoleScreenQuery : IRequest<List<SearchedRoleScreenModel>>
    {

        public int? ID { get; set; }
        public int RoleID { get; set; }


    }

    public class GetRoleScreenQueryHandler : IRequestHandler<GetRoleScreenQuery, List<SearchedRoleScreenModel>>
    {
        private readonly AppIdentityDbContext _dbContext;

        public GetRoleScreenQueryHandler(AppIdentityDbContext identityContext)
        {
            _dbContext = identityContext;
        }
        public async Task<List<SearchedRoleScreenModel>> Handle(GetRoleScreenQuery request, CancellationToken cancellationToken)
        {


            List<SearchedRoleScreenModel> fresult = new List<SearchedRoleScreenModel>();

            if (request.ID.HasValue)
            {
                var RoleId = _dbContext.RoleScreens.Where(e => e.Id == request.ID).SingleOrDefault().RoleId;
                string roleName = _dbContext.Roles.Where(r => r.Id == RoleId).SingleOrDefault().Name;

                fresult = await (
                   from rs in _dbContext.RoleScreens
                   join s in _dbContext.Screens on rs.ScreenId equals s.Id
                   where rs.Id == request.ID
                   select new SearchedRoleScreenModel
                   {
                       ID = rs.Id,
                       RoleID = rs.RoleId,
                       ScreenID = rs.ScreenId,
                       ScreenName = s.Title,
                       RoleName = roleName,
                       Description = s.Description,
                       DirectoryPath = s.DirectoryPath,
                       Icon = s.Icon,
                       ParentID = s.ParentId,
                       ModuleID = s.ModuleId,
                       Sorter = s.Sorter
                   }).ToListAsync();
            }
            else
            {


                string roleName = _dbContext.Roles.Where(r => r.Id == request.RoleID).SingleOrDefault().Name;

                fresult = await (
                   from rs in _dbContext.RoleScreens
                   join s in _dbContext.Screens on rs.ScreenId equals s.Id
                   where rs.RoleId == request.RoleID
                   select new SearchedRoleScreenModel
                   {
                       ID = rs.Id,
                       RoleID = rs.RoleId,
                       ScreenID = rs.ScreenId,
                       ScreenName = s.Title,
                       RoleName = roleName,
                       Description = s.Description,
                       DirectoryPath = s.DirectoryPath,
                       Icon = s.Icon,
                       ParentID = s.ParentId,
                       ModuleID = s.ModuleId,
                       Sorter = s.Sorter
                   }).ToListAsync();
            }

            return fresult;
        }
    }
}
