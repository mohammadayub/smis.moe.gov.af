using App.Application.Management.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Queries
{
    public class GetDisabledReasonQuery : IRequest<List<DisabledReasonModel>>
    {
        public int? ID { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetDisabledReasonQueryHandler : IRequestHandler<GetDisabledReasonQuery, List<DisabledReasonModel>>
    {
        private AppDbContext Context { get; }
        public GetDisabledReasonQueryHandler(AppDbContext dbContext)
        {
            Context = dbContext;
        }
        public async Task<List<DisabledReasonModel>> Handle(GetDisabledReasonQuery request, CancellationToken cancellationToken)
        {

            var query = Context.DisabledReasons.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if(request.IsActive.HasValue)
            {
                query = query.Where(e => e.IsActive == request.IsActive);
            }

            var list = await query.Select(e => new DisabledReasonModel
            {
                ID = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                IsActive = e.IsActive
            }).ToListAsync();

            return list;
        }
    }
}
