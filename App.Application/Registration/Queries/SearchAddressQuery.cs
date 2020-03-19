using App.Application.Registration.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Queries
{
    public class SearchAddressQuery : IRequest<List<SearchAddressModel>>
    {
        public int? ID { get; set; }
    }

    public class SearchAddressQueryHandler : IRequestHandler<SearchAddressQuery, List<SearchAddressModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchAddressQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchAddressModel>> Handle(SearchAddressQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Addresses
                .Include(e => e.Country)
                .Include(e => e.Province)
                .Where(e => e.StatusId == 1)
                .AsQueryable();

            if(request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new SearchAddressModel
            {
                Id = e.Id,
                CountryId = e.CountryId,
                ProvinceId = e.ProvinceId,
                DistrictId = e.DistrictId,
                City = e.City,
                Village = e.Village,
                Detail = e.Detail,
                AddressTypeId = e.AddressTypeId,
                ProfileId = e.ProvinceId,
                Country = e.Country.Title,
                Province = e.Province.TitleEn,
                AddressType = e.AddressType.NameEn
            }).ToListAsync();
        }
    }
}
