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
    public class GetBlackListReasonsQuery : IRequest<List<BlackListReasonModel>>
    {
        public int? ID { get; set; }
        public bool ActiveOnly { get; set; } = true;
    }

    public class GetBlackListReasonsQueryHandler : IRequestHandler<GetBlackListReasonsQuery, List<BlackListReasonModel>>
    {
        private AppDbContext Context { get; set; }
        public GetBlackListReasonsQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<BlackListReasonModel>> Handle(GetBlackListReasonsQuery request, CancellationToken cancellationToken)
        {
            var query = Context.BlackListReasons.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ActiveOnly)
            {
                query = query.Where(e => e.IsActive == true);
            }

            return await query.Select(e => new BlackListReasonModel
            {
                ID = e.Id,
                Name = e.Title,
                NameEn = e.TitleEn
            }).ToListAsync();
        }
    }
}
