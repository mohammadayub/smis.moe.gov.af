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
    public class GetRequestTypeList : IRequest<List<RequestTypeModel>>
    {
        public int? ID { get; set; }
    }

    public class GetRequestTypeListHandler : IRequestHandler<GetRequestTypeList, List<RequestTypeModel>>
    {
        public AppDbContext Context { get; set; }
        public GetRequestTypeListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<RequestTypeModel>> Handle(GetRequestTypeList request, CancellationToken cancellationToken)
        {
            var query = Context.RequestType.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            return await query.Select(e => new RequestTypeModel
            {
                ID = e.Id,
                Name = e.Title,
                Priority = e.Priority
            }).ToListAsync();
        }
    }
}
