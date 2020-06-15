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
    public class GetOfficesQuery:IRequest<List<SearchOfficeModel>>
    {
        public int? ID { get; set; }
    }

    public class GetOfficesQueryHandler : IRequestHandler<GetOfficesQuery, List<SearchOfficeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetOfficesQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchOfficeModel>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Offices.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new SearchOfficeModel
            {
                ID = e.Id,
                Code = e.Code,
                Title = e.Title,
                TitleEn = e.TitleEn,
                CountryId = e.CountryId,
                //CurrencyId = e.CurrencyId,
                OfficeTypeId = e.OfficeTypeId,
                OrganizationId = e.OrganizationId,
                ProvinceId = e.ProvinceId,
                Country = e.Country.Title,
                Province = e.Province.TitleEn
            }).ToListAsync();
        }
    }

}
