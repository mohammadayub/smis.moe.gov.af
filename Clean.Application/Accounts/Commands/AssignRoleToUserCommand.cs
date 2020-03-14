using Clean.Application.Accounts.Models;
using Clean.Application.Accounts.Queries;
using Clean.Common.Exceptions;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Commands
{
    public class AssignRoleToUserCommand : IRequest<List<SearchedUserInRoleModel>>
    {
        public int UserId { get; set; }
        public int RoleID { get; set; }
    }
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, List<SearchedUserInRoleModel>>
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly IMediator _mediator;
        public AssignRoleToUserCommandHandler(AppIdentityDbContext identityContext, IMediator mediator)
        {
            _dbContext = identityContext;
            _mediator = mediator;

        }
        public async Task<List<SearchedUserInRoleModel>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {


            List<SearchedUserInRoleModel> fresult = new List<SearchedUserInRoleModel>();
            using (_dbContext)
            {


                if (await _dbContext.UserRoles.Where(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleID).AnyAsync())
                    throw new BusinessRulesException("این نقش قبلا به کاربر تعین گردیده است");

                _dbContext.UserRoles.Add(new AppUserRole() { UserId = request.UserId, RoleId = request.RoleID });
                await _dbContext.SaveChangesAsync(cancellationToken);
                fresult = await _mediator.Send(new GetUserRolesQuery() { UserID = request.UserId, RoleId = request.RoleID });
            }

            return fresult;
        }
    }
}
