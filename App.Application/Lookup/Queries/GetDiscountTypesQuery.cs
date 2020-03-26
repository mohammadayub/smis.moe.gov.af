using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetDiscountTypesQuery : IRequest<List<DiscountTypeModel>>
    {
    }

    public class GetDiscountTypesQueryHandler : IRequestHandler<GetDiscountTypesQuery, List<DiscountTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetDiscountTypesQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<DiscountTypeModel>> Handle(GetDiscountTypesQuery request, CancellationToken cancellationToken)
        {
            return await Context.DiscountTypes.Select(e => new DiscountTypeModel
            {
                ID = e.Id,
                Name = e.Title,
                Code = e.Code
            }).ToListAsync();

        }
    }
}