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
    public class SavePaymentPenaltyCommand : IRequest<List<PaymentPenaltyModel>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public int OfficeId { get; set; }
        public bool IsActive { get; set; }
    }


    public class SavePaymentPenaltyCommandHandler : IRequestHandler<SavePaymentPenaltyCommand, List<PaymentPenaltyModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SavePaymentPenaltyCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PaymentPenaltyModel>> Handle(SavePaymentPenaltyCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.PaymentPenalties.Where(e => e.Id == request.Id).Single() : new Domain.Entity.pas.PaymentPenalty();

            cur.OfficeId = request.OfficeId;
            cur.Title = request.Title;
            cur.Amount = request.Amount;
            cur.StatusId = request.IsActive ? 1 : 0;

            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.PaymentPenalties.Add(cur);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchPaymentPenaltyQuery { ID = cur.Id });
        }
    }
}
