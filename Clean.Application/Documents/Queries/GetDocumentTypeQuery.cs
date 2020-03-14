using Clean.Application.Documents.Models;
using Clean.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Documents.Queries
{
    public class GetDocumentTypeQuery : IRequest<List<SearchDocumentTypeModel>>
    {
        public int? ID { get; set; }
        public string Catagory { get; set; }
        public int? ScreenID { get; set; }
    }
    public class GetDocumentTypeQueryHandler : IRequestHandler<GetDocumentTypeQuery, List<SearchDocumentTypeModel>>
    {
        public BaseContext Context { get; set; }
        public GetDocumentTypeQueryHandler(BaseContext context)
        {
            Context = context;
        }
        public async Task<List<SearchDocumentTypeModel>> Handle(GetDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            List<SearchDocumentTypeModel> list = new List<SearchDocumentTypeModel>();
            var query = Context.ScreenDocuments
                .Include(e => e.DocumentType)
                .AsQueryable();

            if (request.ScreenID.HasValue)
            {
                query = query.Where(e => e.ScreenId == request.ScreenID);
            }
            if (!String.IsNullOrEmpty(request.Catagory))
            {
                query = query.Where(e => EF.Functions.ILike(e.DocumentType.Category, String.Concat("%", request.Catagory, "%")));
            }
            
            return await query.Select(f => f.DocumentType).Select(f => new SearchDocumentTypeModel { 
                Id = f.Id,
                Name = f.Name
            }).ToListAsync();
        }
    }
}
