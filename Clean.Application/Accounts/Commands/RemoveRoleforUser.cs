using Clean.Application.Accounts.Models;
using Clean.Application.Accounts.Queries;
using Clean.Persistence.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Commands
{
    public class RemoveRoleforUser : IRequest<List<SearchedUserInRoleModel>>
    {
        public string Id { get; set; }

    }

    public class RemoveRoleforUserHandler : IRequestHandler<RemoveRoleforUser, List<SearchedUserInRoleModel>>
    {
        public IMediator _mediator { get; set; }
        private readonly AppIdentityDbContext _Context;
        public RemoveRoleforUserHandler(IMediator mediator, AppIdentityDbContext context)
        {
            _mediator = mediator;
            _Context = context;
        }
        public async Task<List<SearchedUserInRoleModel>> Handle(RemoveRoleforUser request, CancellationToken cancellationToken)
        {
            List<SearchedUserInRoleModel> result = new List<SearchedUserInRoleModel>();
            string[] record = request.Id.Split('_');
            int[] convertedid = Array.ConvertAll<string, int>(record, int.Parse);
            var userrole = _Context.UserRoles.Where(ur => ur.UserId == convertedid[0] && ur.RoleId == convertedid[1]).FirstOrDefault();
            _Context.UserRoles.Remove(userrole);
            await _Context.SaveChangesAsync();
            result = await _mediator.Send(new GetUserRolesQuery() { UserID = convertedid[0] });
            return result;



        }
    }
}
