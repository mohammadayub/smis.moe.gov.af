using App.Application.QualityControl.Models;
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

namespace App.Application.QualityControl.Queries
{
    public class SearchApplicationQuery : IRequest<List<PassportApplicationModel>>
    {
        public int? ID { get; set; }
        public int? PassportPrintID { get; set; }

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
                if (request.PassportPrintID.HasValue)
                {
                    query = Context.PassportPrints.Where(e => e.Id == request.PassportPrintID).Select(e => e.PrintQueue.Application);
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
                PaymentCategory = e.PaymentCategory.Title,
                PaymentMethod = e.PaymentMethod.Title,
                PaymentPenalty = e.PaymentPenalty.Title,
                RequestType = e.RequestType.Title,
                StatusId = e.StatusId

            }).ToListAsync();

        }
    }
}
