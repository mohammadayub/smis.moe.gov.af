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
    public class GetGenderList : IRequest<List<GenderModel>>
    {
        public int? ID { get; set; }
    }

    public class GetGenderListHandler : IRequestHandler<GetGenderList, List<GenderModel>>
    {
        private AppDbContext Context { get; set; }
        public GetGenderListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<GenderModel>> Handle(GetGenderList request, CancellationToken cancellationToken)
        {
            var query = Context.Genders.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new GenderModel
            {
                ID = e.Id,
                Name = e.Name
            }).ToListAsync();
        }
    }
}
