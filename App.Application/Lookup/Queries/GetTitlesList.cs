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
    public class GetTitlesList : IRequest<List<TitleModel>>
    {
        public int? ID { get; set; }
    }

    public class GetTitlesListHandler : IRequestHandler<GetTitlesList, List<TitleModel>>
    {
        private AppDbContext Context { get; set; }
        public GetTitlesListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<TitleModel>> Handle(GetTitlesList request, CancellationToken cancellationToken)
        {
            var query = Context.PersonTitles.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            

            return await query.Select(e => new TitleModel
            {
                ID = e.Id,
                Name = e.Name,
                NameEn = e.NameEn
            }).ToListAsync();
        }
    }
}
