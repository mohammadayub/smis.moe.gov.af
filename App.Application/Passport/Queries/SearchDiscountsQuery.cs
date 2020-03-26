using App.Application.Passport.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
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
    public class SearchDiscountsQuery : IRequest<List<PassportDiscountsModel>>
    {
        public int? ID { get; set; }
        public int? OfficeID { get; set; }
        public bool LimitByUser { get; set; } = false;
        public bool ActiveOnly {get;set;} = false;
    }

    public class SearchDiscountsQueryHandler : IRequestHandler<SearchDiscountsQuery, List<PassportDiscountsModel>>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        public SearchDiscountsQueryHandler(AppDbContext context,ICurrentUser currentUser)
        {
            Context = context;
            CurrentUser = currentUser;
        }
        public async Task<List<PassportDiscountsModel>> Handle(SearchDiscountsQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Discounts.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (request.LimitByUser)
                {
                    var UserOffice = await CurrentUser.GetOfficeID();
                    query = query.Where(e => e.OfficeId == UserOffice);
                }
                if(request.ActiveOnly){
                    query = query.Where(e => e.IsActive && (e.ActiveTo == null || e.ActiveTo <= DateTime.Now.Date));
                }
                if (request.OfficeID.HasValue)
                {
                    query = query.Where(e => e.OfficeId == request.OfficeID);
                }
            }

            return await query.Select(e => new PassportDiscountsModel
            {
                Id = e.Id,
                Name = e.Name,
                DiscountTypeId = e.DiscountTypeId,
                DiscountType = e.DiscountType.Title,
                IsActive = e.IsActive,
                Amount = e.Amount,
                ActiveFrom = e.ActiveFrom.ToString("yyyy-MM-dd"),
                ActiveFromShamsi = PersianDate.ToPersianDate(e.ActiveFrom),
                ActiveTo = e.ActiveTo.HasValue ? e.ActiveTo.Value.ToString("yyyy-MM-dd") : "",
                ActiveToShamsi = e.ActiveTo.HasValue ? PersianDate.ToPersianDate(e.ActiveTo.Value) : "",
                OfficeId = e.OfficeId,
                Office = e.Office.TitleEn

            }).ToListAsync();

        }
    }
}
