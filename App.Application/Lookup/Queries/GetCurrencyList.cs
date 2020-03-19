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
    public class GetCurrencyList : IRequest<List<CurrencyModel>>
    {

    }

    public class GetCurrencyListHandler : IRequestHandler<GetCurrencyList, List<CurrencyModel>>
    {
        public AppDbContext Context { get; set; }
        public GetCurrencyListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<CurrencyModel>> Handle(GetCurrencyList request, CancellationToken cancellationToken)
        {
            return await Context.Currencies.Select(e => new CurrencyModel
            {
                ID = e.Id,
                Code = e.Code,
                Name = e.TitleEn
            }).ToListAsync();
        }
    }
}
