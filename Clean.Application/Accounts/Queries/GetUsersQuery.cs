using Clean.Application.Accounts.Models;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Queries
{
    public class GetUsersQuery : IRequest<List<SearchedUsersModel>>
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public int? OrganizationID { get; set; }
        // Only when user is created
        public string GeneratedPassword { get; set; }

    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<SearchedUsersModel>>
    {
        private readonly AppIdentityDbContext _dbContext;
        public GetUsersQueryHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SearchedUsersModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<SearchedUsersModel> result = new List<SearchedUsersModel>();

            var query = _dbContext.Users
                .Include(e => e.Office)
                .AsQueryable();

            if (request.Id.HasValue && request.Id != default(int))
            {
                query = query.Where(u => u.Id == request.Id);
            }
            else if (request.OrganizationID.HasValue && !string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(u => u.OrganizationID == request.OrganizationID && EF.Functions.Like(u.UserName, string.Concat("%", request.UserName, "%")));
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(u => EF.Functions.Like(u.UserName, string.Concat("%", request.UserName, "%")));
            }
            else
            {
                query = query.Take(100);
            }

            return await query
                .Select(e => new SearchedUsersModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    FatherName = e.FatherName,
                    Organization = string.Empty,
                    UserName = e.UserName,
                    Email = e.Email,
                    OfficeID = e.OfficeID,
                    GeneratedPassword = string.IsNullOrEmpty(request.GeneratedPassword) ? null : request.GeneratedPassword,
                    OrganizationID = e.OrganizationID,
                    Id = e.Id,
                    Active = !e.Disabled
                }).ToListAsync(cancellationToken);
        }
    }
}
