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
    public class GetPaymentCategoryQuery :IRequest<List<PaymentCategoryModel>>
    {

    }

    public class PaymentCategoryModelHandler : IRequestHandler<GetPaymentCategoryQuery, List<PaymentCategoryModel>>
    {
        private AppDbContext Context { get; set; }
        public PaymentCategoryModelHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PaymentCategoryModel>> Handle(GetPaymentCategoryQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PaymentCategories.AsQueryable();

            return await query.Select(e => new PaymentCategoryModel
            {
                Id = e.Id,
                Title = e.Title
            }).ToListAsync();
        }
    }
}
