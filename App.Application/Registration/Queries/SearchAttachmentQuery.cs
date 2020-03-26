using App.Application.Registration.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Queries
{
    public class SearchAttachmentQuery: IRequest<List<AttachmentModel>>
    {
        public long? ID { get; set; }
        public int? ProfileID { get; set; }
    }

    public class SearchAttachmentQueryHandler : IRequestHandler<SearchAttachmentQuery, List<AttachmentModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchAttachmentQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<AttachmentModel>> Handle(SearchAttachmentQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Attachments.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProfileID.HasValue)
            {
                query = query.Where(e => e.ProfileId == request.ProfileID);
            }

            return await query.Select(e => new AttachmentModel
            {
                Id = e.Id,
                Name = e.Name,
                AttachmentTypeId = e.AttachmentTypeId,
                AttachmentType = e.AttachmentType.Title,
                ContentType = e.ContentType,
                Path = e.Path,
                ProfileId = e.ProfileId,
                Description = e.Description,
                DocumentDate = e.DocumentDate.ToString("yyyy-MM-dd"),
                DocumentDateShamsi = PersianDate.ToPersianDate(e.DocumentDate),
                DocumentNumber = e.DocumentNumber,
                EncryptionKey = e.EncryptionKey,
                IsEncrypted = e.IsEncrypted,
                StatusId = e.StatusId
            }).ToListAsync();
        }
    }
}
