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
    public class SavePaymentConfigCommand : IRequest<List<PaymentConfigModel>>
    {
        public int? Id { get; set; }
        public int OfficeId { get; set; }
        public int PassportTypeId { get; set; }
        public int PassportDurationId { get; set; }
        public int PaymentCategoryId { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
    }


    public class SavePaymentConfigCommandHandler : IRequestHandler<SavePaymentConfigCommand, List<PaymentConfigModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SavePaymentConfigCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PaymentConfigModel>> Handle(SavePaymentConfigCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.PaymentConfigs.Where(e => e.Id == request.Id).Single() : new Domain.Entity.pas.PaymentConfig();

            cur.OfficeId = request.OfficeId;
            cur.PassportTypeId = request.PassportTypeId;
            cur.PassportDurationId = request.PassportDurationId;
            cur.PaymentCategoryId = request.PaymentCategoryId;
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
                Context.PaymentConfigs.Add(cur);
            }

            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchPaymentConfigQuery { ID = cur.Id });
        }
    }
}
