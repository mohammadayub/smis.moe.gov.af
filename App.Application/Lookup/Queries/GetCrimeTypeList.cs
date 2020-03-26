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
    public class GetCrimeTypeList : IRequest<List<CrimeTypeModel>>
    {
        public int? ID { get; set; }
    }

    public class GetCrimeTypeListHandler : IRequestHandler<GetCrimeTypeList, List<CrimeTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetCrimeTypeListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<CrimeTypeModel>> Handle(GetCrimeTypeList request, CancellationToken cancellationToken)
        {
            var query = Context.CrimeTypes.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            return await query.Select(e => new CrimeTypeModel
            {
                ID = e.Id,
                Title = e.Title
            }).ToListAsync();
        }
    }
}
