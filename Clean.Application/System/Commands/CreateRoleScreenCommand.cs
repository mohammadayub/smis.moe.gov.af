using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Common.Exceptions;
using Clean.Domain.Entity.look;
using Clean.Persistence.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Commands
{
    public class CreateRoleScreenCommand : IRequest<List<SearchedRoleScreenModel>>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ScreenID { get; set; }
    }
    public class CreateRoleScreenCommandHandler : IRequestHandler<CreateRoleScreenCommand, List<SearchedRoleScreenModel>>
    {
        AppIdentityDbContext Context { get; set; }
        IMediator Mediator { get; set; }
        public CreateRoleScreenCommandHandler(AppIdentityDbContext context, IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
        }
        public async Task<List<SearchedRoleScreenModel>> Handle(CreateRoleScreenCommand request, CancellationToken cancellationToken)
        {
            List<SearchedRoleScreenModel> fresult = new List<SearchedRoleScreenModel>();
            using (Context)
            {
                if (Context.RoleScreens.Where(rs => rs.RoleId == request.RoleId && rs.ScreenId == request.ScreenID).Any())
                    throw new BusinessRulesException("صفحه انتخاب شده قبلا به نقش اضافه گردیده است");

                RoleScreen roleScreen = new RoleScreen() { 
                    RoleId = request.RoleId, 
                    ScreenId = request.ScreenID, 
                    IsActive = true };
                Context.RoleScreens.Add(roleScreen);
                await Context.SaveChangesAsync(cancellationToken);
                fresult = await Mediator.Send(new GetRoleScreenQuery() { RoleID = request.RoleId });
            }
            return fresult;
        }
    }
}
