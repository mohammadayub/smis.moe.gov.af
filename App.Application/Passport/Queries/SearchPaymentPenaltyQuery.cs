using App.Application.Passport.Models;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Passport.Queries
{
    public class SearchPaymentPenaltyQuery : IRequest<List<PaymentPenaltyModel>>
    {
        public int? ID { get; set; }
        public int? OfficeID { get; set; }
        public int? StatusID { get; set; }
        public bool LimitByUser { get; set; } = false;
    }

    public class SearchPaymentPenaltyQueryHandler : IRequestHandler<SearchPaymentPenaltyQuery, List<PaymentPenaltyModel>>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        public SearchPaymentPenaltyQueryHandler(AppDbContext context,ICurrentUser currentUser)
        {
            Context = context;
            CurrentUser = currentUser;
        }
        public async Task<List<PaymentPenaltyModel>> Handle(SearchPaymentPenaltyQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PaymentPenalties.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (request.LimitByUser)
                {
                    var office = await CurrentUser.GetOfficeID();
                    query = query.Where(e => e.OfficeId == office);
                }
                if (request.OfficeID.HasValue)
                {
                    query = query.Where(e => e.OfficeId == request.OfficeID);
                }
                if (request.StatusID.HasValue)
                {
                    query = query.Where(e => e.StatusId == request.StatusID);
                }
            }

            return await query.Select(e => new PaymentPenaltyModel
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                IsActive = e.StatusId == 1,
                OfficeId = e.OfficeId,
                Office = e.Office.TitleEn,
                Status = e.StatusId == 1 ? "فعال" : "غیر فعال"
            }).ToListAsync();
        }
    }
}
