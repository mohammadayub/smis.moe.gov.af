using Clean.Application.Documents.Models;
using Clean.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Clean.Common.Dates;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Documents.Queries
{
    public class SearchDocumentQuery : IRequest<List<SearchedDocumentModel>>
    {

        public long? Id { get; set; }
        public int RecordId { get; set; }

    }


    public class SearchDocumentQueryHandler : IRequestHandler<SearchDocumentQuery, List<SearchedDocumentModel>>
    {

        private readonly BaseContext _context;
        //private readonly IMediator _mediator;

        public SearchDocumentQueryHandler(BaseContext context/*, IMediator mediator*/)
        {
            _context = context;
            //_mediator = mediator;
        }

        public async Task<List<SearchedDocumentModel>> Handle(SearchDocumentQuery request, CancellationToken cancellationToken)
        {
            List<SearchedDocumentModel> result = new List<SearchedDocumentModel>();
            if (request.Id != null)
            {
                result = await (from d in _context.Documents
                                    //join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id into ddt
                                    //from resultDDT in ddt.DefaultIfEmpty()
                                where d.Id == request.Id
                                select new SearchedDocumentModel
                                {
                                    Id = request.Id,
                                    Description = d.Description,
                                    DocumentTypeId = d.DocumentTypeId,
                                    DocumentTypeText = d.DocumentType.Name,
                                    Path = d.Path,
                                    DocumentNumber = d.DocumentNumber,
                                    DocumentSource = d.DocumentSource,
                                    DocumentDateShamsi = PersianDate.GetFormatedString(d.DocumentDate),
                                    DownloadDateText = PersianDate.GetFormatedString(d.LastDownloadDate),
                                    UploadDateText = PersianDate.GetFormatedString(d.UploadDate)

                                }).ToListAsync(cancellationToken);
            }
            else /*if (request.RecordId != null)*/
            {
                result = await (from d in _context.Documents
                                    //join dt in _context.DocumentTypes on d.DocumentTypeId equals dt.Id into ddt
                                    //from resultDDT in ddt.DefaultIfEmpty()
                                where (d.RecordId == request.RecordId)
                                select new SearchedDocumentModel
                                {
                                    Id = d.Id,
                                    Description = d.Description,
                                    DocumentTypeId = d.DocumentTypeId,
                                    DocumentTypeText = d.DocumentType.Name,
                                    Path = d.Path,
                                    DocumentNumber = d.DocumentNumber,
                                    DocumentSource = d.DocumentSource,
                                    DocumentDateShamsi = PersianDate.GetFormatedString(d.DocumentDate),
                                    DownloadDateText = PersianDate.GetFormatedString(d.LastDownloadDate),
                                    UploadDateText = PersianDate.GetFormatedString(d.UploadDate)
                                }).ToListAsync(cancellationToken);
            }
            return result;

        }
    }
}
