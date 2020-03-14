using Clean.Application.Documents.Models;
using Clean.Application.Documents.Queries;
using Clean.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Clean.Application.Documents.Commands
{
    public class CreateDocumentCommand : IRequest<List<SearchedDocumentModel>>
    {

        // public Person Person { get; set; }

        public long? Id { get; set; }

        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Module { get; set; }
        public string Item { get; set; }
        public long RecordId { get; set; }
        public string Root { get; set; }
        public string Path { get; set; }
        public string EncryptionKey { get; set; }
        public string ReferenceNo { get; set; }
        public int? StatusId { get; set; }
        public int? ScreenId { get; set; }
        public string Description { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime? LastDownloadDate { get; set; }
        public string FileName { get; set; }
        public string DocumentNumber { get; set; }
        public string Documentsource { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int? BranchId { get; set; }//just for account opening


    }



    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, List<SearchedDocumentModel>>
    {

        private readonly BaseContext _context;
        private readonly IMediator _mediator;
        public CreateDocumentCommandHandler(BaseContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<List<SearchedDocumentModel>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            List<SearchedDocumentModel> result = new List<SearchedDocumentModel>();
            if (request.Id == null || request.Id == default(decimal))
            {

                var d = new Clean.Domain.Entity.doc.Documents()
                {
                    FileName = request.Item,
                    ContentType = request.ContentType,
                    UploadDate = DateTime.Now,
                    RecordId = request.RecordId,
                    Root = request.Root,
                    Path = request.Path,
                    Description = request.Description,
                    EncryptionKey = request.EncryptionKey,
                    StatusId = request.StatusId,
                    ScreenId = request.StatusId,
                    LastDownloadDate = DateTime.Now,
                    DocumentNumber = request.DocumentNumber,
                    DocumentDate = request.DocumentDate,
                    DocumentSource = request.Documentsource,
                    DocumentTypeId = request.DocumentTypeId,
                };
                _context.Documents.Add(d);

                await _context.SaveChangesAsync(cancellationToken);

                result = await _mediator.Send(new SearchDocumentQuery() { Id = d.Id });


            }
            // Update
            else
            {

                var d = (from p in _context.Documents
                                              where p.Id == request.Id
                                              select p).First();

                d.FileName = request.Item;
                d.ContentType = request.ContentType;
                d.Root = request.Root;

                d.Path = request.Path;
                d.RecordId = request.RecordId;
                d.UploadDate = DateTime.Now;
                d.Description = request.Description;
                d.EncryptionKey = request.EncryptionKey;
                d.StatusId = request.StatusId;
                d.ScreenId = request.StatusId;
                d.LastDownloadDate = DateTime.Now;
                d.DocumentNumber = request.DocumentNumber;
                d.DocumentDate = request.DocumentDate;
                d.DocumentSource = request.Documentsource;
                d.DocumentTypeId = request.DocumentTypeId;

                await _context.SaveChangesAsync();

                result = await _mediator.Send(new SearchDocumentQuery() { Id = d.Id });


            }
            return result;
        }
    }
}
