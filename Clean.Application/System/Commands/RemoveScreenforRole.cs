using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Commands
{
    public class RemoveScreenforRole : IRequest<List<SearchedRoleScreenModel>>
    {
        public int? Id { get; set; }
    }

    public class RemoveScreenforRoleHandler : IRequestHandler<RemoveScreenforRole, List<SearchedRoleScreenModel>>
    {
        public IMediator _mediator { get; set; }
        public AppIdentityDbContext _Context { get; set; }
        public RemoveScreenforRoleHandler(IMediator mediator, AppIdentityDbContext context)
        {
            _mediator = mediator;
            _Context = context;
        }
        public async Task<List<SearchedRoleScreenModel>> Handle(RemoveScreenforRole request, CancellationToken cancellationToken)
        {
            List<SearchedRoleScreenModel> result = new List<SearchedRoleScreenModel>();
            var RoleScreen = await _Context.RoleScreens.Where(e => e.Id == request.Id).FirstOrDefaultAsync();
            _Context.RoleScreens.Remove(RoleScreen);
            await _Context.SaveChangesAsync();
            result = await _mediator.Send(new GetRoleScreenQuery() { RoleID = RoleScreen.RoleId });
            return result;
        }
    }
}
