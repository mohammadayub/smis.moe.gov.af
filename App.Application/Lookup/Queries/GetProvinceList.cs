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
    public class GetProvinceList : IRequest<List<ProvinceModel>>
    {
        public int? ID { get; set; }
        public int? CountryID { get; set; }
        public string Code { get; set; }
    }

    public class GetProvinceListHandler : IRequestHandler<GetProvinceList, List<ProvinceModel>>
    {
        private AppDbContext Context { get; set; }
        public GetProvinceListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<ProvinceModel>> Handle(GetProvinceList request, CancellationToken cancellationToken)
        {
            var query = Context.Provinces
                .Include(e => e.Country)
                .AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.CountryID.HasValue)
            {
                query = query.Where(e => e.CountryId == request.CountryID);
            }
            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => EF.Functions.ILike(e.Code, String.Concat("%", request.Code, "%")));
            }

            return await query.Select(e => new ProvinceModel
            {
                ID = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                Code = e.Code,
                Country = e.Country.Title,
                CountryID = e.CountryId
            }).ToListAsync();

        }
    }
}
