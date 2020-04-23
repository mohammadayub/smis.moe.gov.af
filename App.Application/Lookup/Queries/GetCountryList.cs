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
    public class GetCountryList : IRequest<List<CountryModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
    }

    public class GetCountryListHandler : IRequestHandler<GetCountryList, List<CountryModel>>
    {
        private AppDbContext Context { get; set; }
        public GetCountryListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<CountryModel>> Handle(GetCountryList request, CancellationToken cancellationToken)
        {
            var query = Context.Countries.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }

            return await query.Select(e => new CountryModel
            {
                ID = e.Id,
                Title = e.TitleEn,
                TitleLocal = e.Title,
                Code = e.Code
            }).ToListAsync();
        }
    }
}
