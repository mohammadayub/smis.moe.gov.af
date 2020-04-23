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
    public class GetStatusListQuery : IRequest<List<StatusModel>>
    {
        public int? ID { get; set; }
        public string StatusType { get; set; }
    }

    public class GetStatusListQueryHandler : IRequestHandler<GetStatusListQuery, List<StatusModel>>
    {
        private AppDbContext Context { get; set; }
        public GetStatusListQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<StatusModel>> Handle(GetStatusListQuery request, CancellationToken cancellationToken)
        {
            var query = Context.SystemStatus
                .Where(e => e.IsActive)
                .AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (!String.IsNullOrEmpty(request.StatusType))
            {
                query = query.Where(e => e.StatusType == request.StatusType);
            }

            return await query.Select(e => new StatusModel
            {
                ID = e.TypeId,
                GID = e.Id,
                Title = e.Title,
                StatusType = e.StatusType,
                Code = e.Code,
                Sorter = e.Sorter,
                IsActive = e.IsActive
            }).ToListAsync();
        }
    }
}
