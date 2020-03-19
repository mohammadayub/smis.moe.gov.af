using App.Application.Account.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Account.Queries
{
    public class GetUserPrintOfficeQuery : IRequest<List<SearchUserPrintOfficeModel>>
    {
        public int? ID { get; set; }
        public int? UserID { get; set; }
    }
    public class GetUserPrintOfficeQueryHandler : IRequestHandler<GetUserPrintOfficeQuery, List<SearchUserPrintOfficeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetUserPrintOfficeQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchUserPrintOfficeModel>> Handle(GetUserPrintOfficeQuery request, CancellationToken cancellationToken)
        {
            var query = Context.UserOfficePrinters.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.UserID.HasValue)
            {
                query = query.Where(e => e.UserId == request.UserID);
            }

            return await query.Select(e => new SearchUserPrintOfficeModel
            {
                ID = e.Id,
                UserID = e.UserId,
                OfficeID = e.OfficeId,
                OfficeName = e.Office.Title
            }).ToListAsync();

        }
    }
}
