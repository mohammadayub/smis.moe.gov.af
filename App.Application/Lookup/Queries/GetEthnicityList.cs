using App.Application.Lookup.Models;
using App.Domain.Entity.look;
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
    public class GetEthnicityList : IRequest<List<Ethnicity>>
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
    }

    public class GetEthnicityListHandler : IRequestHandler<GetEthnicityList, List<Ethnicity>>
    {
        private readonly AppDbContext Context;
        public GetEthnicityListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<Ethnicity>> Handle(GetEthnicityList request, CancellationToken cancellationToken)
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
            return await query.ToListAsync();
        }
    }

}
