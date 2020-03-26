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
    public class SaveDiscountsCommand : IRequest<List<PassportDiscountsModel>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int DiscountTypeId { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public int OfficeId { get; set; }
    }


    public class SaveDiscountsCommandHandler : IRequestHandler<SaveDiscountsCommand, List<PassportDiscountsModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveDiscountsCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<PassportDiscountsModel>> Handle(SaveDiscountsCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.Discounts.Where(e => e.Id == request.Id).Single() : new Domain.Entity.pas.Discounts();

            cur.Name = request.Name;
            cur.DiscountTypeId = request.DiscountTypeId;
            cur.OfficeId = request.OfficeId;
            cur.IsActive = request.IsActive;
            cur.Amount = request.Amount;
            cur.ActiveFrom = request.ActiveFrom;
            cur.ActiveTo = request.ActiveTo;

            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.Discounts.Add(cur);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchDiscountsQuery { ID = cur.Id });
        }
    }
}
