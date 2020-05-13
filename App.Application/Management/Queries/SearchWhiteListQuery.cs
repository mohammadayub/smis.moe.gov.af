using App.Application.Management.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Queries
{
    public class SearchWhiteListQuery : IRequest<List<SearchWhiteListModel>>
    {
        public int? ID { get; set; }
        public int? BlackListID { get; set; }
    }


    public class SearchWhiteListQueryHandler : IRequestHandler<SearchWhiteListQuery, List<SearchWhiteListModel>>
    {
        private AppDbContext Context { get; }
        public SearchWhiteListQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchWhiteListModel>> Handle(SearchWhiteListQuery request, CancellationToken cancellationToken)
        {
            var query = Context.WhiteLists.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.BlackListID.HasValue)
            {
                query = query.Where(e => e.BlackListId == request.BlackListID);
            }


            return await query.Select(e => new SearchWhiteListModel
            {
                Id = e.Id,
                BlackListId = e.BlackListId,
                WhiteListDate = e.WhiteListDate.ToString("yyyy-MM-dd"),
                WhiteListDateShamsi = PersianDate.ToPersianDate(e.WhiteListDate),
                RequestedById = e.RequestedById,
                RequestedBy = e.RequestedBy.Dari,
                Comments = e.Comments

            }).ToListAsync();
        }
    }
}
