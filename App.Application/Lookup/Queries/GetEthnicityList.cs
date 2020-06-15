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
    public class GetEthnicityList : IRequest<List<EthnicityModel>>
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
    }

    public class GetEthnicityListHandler : IRequestHandler<GetEthnicityList, List<EthnicityModel>>
    {
        private readonly AppDbContext Context;
        public GetEthnicityListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<EthnicityModel>> Handle(GetEthnicityList request, CancellationToken cancellationToken)
        {

            var query = Context.Ethnicities.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID.Value).AsQueryable();
            }
            else if (request.ParentID.HasValue)
            {
                query = query.Where(e => e.ParentId == request.ParentID).AsQueryable();
            }
            return await query.Select(e => new EthnicityModel
            {
                Id = e.Id,
                CountryId = e.CountryId,
                Name = e.Name,
                ParentId = e.ParentId
            }).ToListAsync();
        }
    }

}
