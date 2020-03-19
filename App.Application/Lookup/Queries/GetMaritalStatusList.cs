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
    public class GetMaritalStatusList : IRequest<List<MaritalStatusModel>>
    {
        public int? ID { get; set; }
    }

    public class GetMaritalStatusListHandler : IRequestHandler<GetMaritalStatusList, List<MaritalStatusModel>>
    {
        private AppDbContext Context { get; set; }
        public GetMaritalStatusListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<MaritalStatusModel>> Handle(GetMaritalStatusList request, CancellationToken cancellationToken)
        {
            var query = Context.MaritalStatus.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new MaritalStatusModel
            {
                ID = e.Id,
                Name = e.Name
            }).ToListAsync();
        }
    }
}
