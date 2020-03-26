using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetPaymentMethodList: IRequest<List<PaymentMethodModel>>
    {
        public int? ID { get; set; }
    }

    public class GetPaymentMethodListHandler : IRequestHandler<GetPaymentMethodList, List<PaymentMethodModel>>
    {
        private AppDbContext Context { get; set; }
        public GetPaymentMethodListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PaymentMethodModel>> Handle(GetPaymentMethodList request, CancellationToken cancellationToken)
        {
            var query = Context.PaymentMethods.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new PaymentMethodModel
            {
                ID = e.Id,
                Name = e.Title,
                HasReceipt = e.HasReceipt
            }).ToListAsync();
        }
    }
}
