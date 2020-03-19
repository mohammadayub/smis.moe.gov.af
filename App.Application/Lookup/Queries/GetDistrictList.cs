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
    public class GetDistrictList : IRequest<List<DistrictModel>>
    {
        public int? ID { get; set; }
        public int? ProvinceID { get; set; }
    }

    public class GetDistrictListHandler : IRequestHandler<GetDistrictList, List<DistrictModel>>
    {
        private AppDbContext Context { get; set; }
        public GetDistrictListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<DistrictModel>> Handle(GetDistrictList request, CancellationToken cancellationToken)
        {
            var query = Context.Districts
                .Include(e => e.Province)
                .AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProvinceID.HasValue)
            {
                query = query.Where(e => e.ProvinceId == request.ProvinceID);
            }

            return await query.Select(e => new DistrictModel
            {
                ID = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                ProvinceID = e.ProvinceId,
                Province = e.Province.TitleEn
            }).ToListAsync();

        }
    }
}
