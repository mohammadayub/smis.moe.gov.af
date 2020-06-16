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
    public class GetLocationList : IRequest<List<LocationModel>>
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
    }

    public class GetLocationListHandler : IRequestHandler<GetLocationList, List<LocationModel>>
    {
        private AppDbContext Context { get; set; }

        public GetLocationListHandler(AppDbContext context)
        {
            Context = context;
        }


        public async Task<List<LocationModel>> Handle(GetLocationList request, CancellationToken cancellationToken)
        {
            var query = Context.Locations.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else if(request.ParentID.HasValue)
            {
                query = query.Where(e => e.ParentId == request.ParentID).AsQueryable();

            }
            return await query.Select(e => new LocationModel

            {
                Id = e.Id,
                Name = e.Name,
                Dari = e.Dari,
                IsActive = e.IsActive,
                Code = e.Code,
                Path = e.Path,
                PathDari = e.PathDari,
                ParentId = e.ParentId,
                TypeId = e.TypeId

            }

            ).ToListAsync();
        }
    }

}
