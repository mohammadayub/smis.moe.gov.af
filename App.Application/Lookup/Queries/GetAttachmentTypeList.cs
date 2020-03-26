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
    public class GetAttachmentTypeList : IRequest<List<AttachmentTypeModel>>
    {
        public int? ID { get; set; }
    }

    public class GetAttachmentTypeListHandler : IRequestHandler<GetAttachmentTypeList, List<AttachmentTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetAttachmentTypeListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<AttachmentTypeModel>> Handle(GetAttachmentTypeList request, CancellationToken cancellationToken)
        {
            var query = Context.AttachmentTypes.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new AttachmentTypeModel
            {
                ID = e.Id,
                Name = e.Title
            }).ToListAsync();
        }
    }
}
