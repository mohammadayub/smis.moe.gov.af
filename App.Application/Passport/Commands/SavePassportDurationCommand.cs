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
    public class SavePassportDurationCommand : IRequest<List<PassportDurationModel>>
    {
        public int? Id { get; set; }
        public int PassportTypeId { get; set; }
        public int Months { get; set; }
    }


    public class SavePassportDurationCommandHandler : IRequestHandler<SavePassportDurationCommand, List<PassportDurationModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SavePassportDurationCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PassportDurationModel>> Handle(SavePassportDurationCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.PassportDurations.Where(e => e.Id == request.Id).Single() : new Domain.Entity.pas.PassportDuration();

            cur.PassportTypeId = request.PassportTypeId;
            cur.Months = request.Months;

            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.PassportDurations.Add(cur);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchPassportDurationQuery { ID = cur.Id });
        }
    }
}
