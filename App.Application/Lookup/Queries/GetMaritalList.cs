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
    public class GetMaritalList : IRequest<List<MaritalModel>>
    {
        public int? ID { get; set; }
    }

    public class GetMaritalListHandler : IRequestHandler<GetMaritalList, List<MaritalModel>>
    {
        private AppDbContext Context { get; set; }
        public GetMaritalListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<MaritalModel>> Handle(GetMaritalList request, CancellationToken cancellationToken)
        {
            var query = Context.MaritalStatus.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new MaritalModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
        }
    }
}
