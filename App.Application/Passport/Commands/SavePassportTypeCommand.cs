using App.Application.Passport.Models;
using App.Application.Passport.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Passport.Commands
{
    public class SavePassportTypeCommand : IRequest<List<PassportTypeModel>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int SerialLength { get; set; }
    }


    public class SavePassportTypeCommandHandler : IRequestHandler<SavePassportTypeCommand, List<PassportTypeModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SavePassportTypeCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PassportTypeModel>> Handle(SavePassportTypeCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.PassportTypes.Where(e => e.Id == request.Id).Single() : new Domain.Entity.pas.PassportType();

            cur.Code = request.Code;
            cur.Name = request.Name;
            cur.SerialLength = request.SerialLength;

            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.PassportTypes.Add(cur);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchPassportTypeQuery { ID = cur.Id });
        }
    }
}
