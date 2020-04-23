using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Printing.Queries
{
    public class SearchApplicationQuery : IRequest<List<PassportApplicationModel>>
    {
        public int? ID { get; set; }
        public long? PrintQueueID { get; set; }

    }

    public class SearchApplicationQueryHandler : IRequestHandler<SearchApplicationQuery, List<PassportApplicationModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchApplicationQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PassportApplicationModel>> Handle(SearchApplicationQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportApplications.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (request.PrintQueueID.HasValue)
                {
                    query = Context.PrintQueues.Where(e => e.Id == request.PrintQueueID).Select(e => e.Application);
                }
            }


            return await query.Select(e => new PassportApplicationModel
            {
                Id = e.Id,
                Code = e.Code,
                PassportTypeId = e.PassportTypeId,
                PassportDurationId = e.PassportDurationId,
                PaymentCategoryId = e.PaymentCategoryId,
                PaymentMethodId = e.PaymentMethodId,
                PaymentPenaltyId = e.PaymentPenaltyId,
                RequestTypeId = e.RequestTypeId,
                BankId = e.BankId,
                ProfileId = e.ProfileId,
                DiscountId = e.DiscountId,
                PaidAmount = e.PaidAmount,
                PaymentDate = e.PaymentDate.ToString("yyyy-MM-dd"),
                ReceiptNumer = e.ReceiptNumer,
                PhotoPath = e.PhotoPath,
                SignaturePath = e.SignaturePath,
                ActiveBioDataId = e.ActiveBioDataId,
                ActiveJobId = e.ActiveJobId,
                ActiveAddressId = e.ActiveAddressId,
                CurProcessId = e.CurProcessId,

                PaymentDateShamsi = PersianDate.ToPersianDate(e.PaymentDate),
                PassportDuration = e.PassportDuration.Months.ToString(),
                PassportType = e.PassportType.Name,
                PassportTypeCode = e.PassportType.Code,
                PaymentCategory = e.PaymentCategory.Title,
                PaymentMethod = e.PaymentMethod.Title,
                PaymentPenalty = e.PaymentPenalty.Title,
                RequestType = e.RequestType.Title,
                StatusId = e.StatusId,

                CreatedBy = e.CreatedBy

            }).ToListAsync();

        }
    }

}
