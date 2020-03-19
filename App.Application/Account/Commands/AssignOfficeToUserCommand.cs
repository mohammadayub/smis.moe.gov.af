using App.Application.Account.Models;
using App.Application.Account.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Account.Commands
{
    public class AssignOfficeToUserCommand : IRequest<List<SearchUserPrintOfficeModel>>
    {
        public int? ID { get; set; }
        public int OfficeID { get; set; }
        public int UserID { get; set; }
    }

    public class AssignOfficeToUserCommandHandler : IRequestHandler<AssignOfficeToUserCommand, List<SearchUserPrintOfficeModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CUser { get; set; }
        public AssignOfficeToUserCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CUser = currentUser;
        }
        public async Task<List<SearchUserPrintOfficeModel>> Handle(AssignOfficeToUserCommand request, CancellationToken cancellationToken)
        {
            var UserOffice = request.ID.HasValue ? Context.UserOfficePrinters.Where(e => e.Id == request.ID).Single() : new Domain.Entity.look.UserOfficePrinter();
            
            UserOffice.OfficeId = request.OfficeID;
            UserOffice.UserId = request.UserID;

            UserOffice.CreatedOn = DateTime.Now;
            UserOffice.CreatedBy = await CUser.GetUserId();

            if (!request.ID.HasValue)
            {
                Context.UserOfficePrinters.Add(UserOffice);
            }

            await Context.SaveChangesAsync();

            return await Mediator.Send(new GetUserPrintOfficeQuery { ID = UserOffice.Id });

        }
    }
}
