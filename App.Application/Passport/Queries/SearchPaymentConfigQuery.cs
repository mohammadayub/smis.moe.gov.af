using App.Application.Passport.Models;
using App.Persistence.Context;
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
    public class SearchPaymentConfigQuery : IRequest<List<PaymentConfigModel>>
    {
        public int? ID { get; set; }
        public int? PassportTypeID { get; set; }
        public int? PassportDurationID { get; set; }
        public int? PaymentCategoryID { get; set; }
        public int? StatusID { get; set; }
    }

    public class SearchPaymentConfigQueryHandler : IRequestHandler<SearchPaymentConfigQuery, List<PaymentConfigModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchPaymentConfigQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PaymentConfigModel>> Handle(SearchPaymentConfigQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PaymentConfigs.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (request.PassportTypeID.HasValue)
                {
                    query = query.Where(e => e.PassportTypeId == request.PassportTypeID);
                }
                if (request.PassportDurationID.HasValue)
                {
                    query = query.Where(e => e.PassportDurationId == request.PassportDurationID);
                }
                if (request.PaymentCategoryID.HasValue)
                {
                    query = query.Where(e => e.PaymentCategoryId == request.PaymentCategoryID);
                }
                if (request.StatusID.HasValue)
                {
                    query = query.Where(e => e.StatusId == request.StatusID);
                }

            }

            return await (from e in query
                         join b in Context.Offices on e.OfficeId equals b.Id
                         select new PaymentConfigModel
                         {
                            Id = e.Id,
                            OfficeId = e.OfficeId,
                            PassportTypeId = e.PassportTypeId,
                            PassportDurationId = e.PassportDurationId,
                            PaymentCategoryId = e.PaymentCategoryId,
                            Amount = e.Amount,
                            IsActive = e.StatusId == 1,
                            Status = e.StatusId == 1 ? "فعال" : "غیر فعال",
                            PassportType = e.PassportType.Name,
                            PassportDuration = String.Concat(e.PassportDuration.Months , " Months"),
                            PaymentCategory = e.PaymentCategory.Title,
                            Office = b.TitleEn
                         }).ToListAsync();
        }
    }
}
