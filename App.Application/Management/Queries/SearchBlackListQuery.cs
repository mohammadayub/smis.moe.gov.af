using App.Application.Management.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Service;
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
    public class SearchBlackListQuery : IRequest<List<SearchBlackListModel>>
    {
        public int? ID { get; set; }
    }


    public class SearchBlackListQueryHandler : IRequestHandler<SearchBlackListQuery, List<SearchBlackListModel>>
    {
        private AppDbContext Context { get; }
        public SearchBlackListQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchBlackListModel>> Handle(SearchBlackListQuery request, CancellationToken cancellationToken)
        {
            var query = Context.BlackLists.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }


            return await query.Select(e => new SearchBlackListModel
            {
                Id = e.Id,
                BlackListProfileId = e.BlackListProfileId,
                BlackListReasonId = e.BlackListReasonId,
                BlackListReason = e.BlackListReason.Title,
                BlackListDate = e.BlackListDate.ToString("yyyy-MM-dd"),
                BlackListDateShamsi = PersianDate.ToPersianDate(e.BlackListDate),
                RequestedById = e.RequestedById,
                RequestedBy = e.RequestedBy.Dari,
                StatusId = e.StatusId,
                PassportNumber = e.PassportNumber,
                Comments = e.Comments

            }).ToListAsync();
        }
    }
}
