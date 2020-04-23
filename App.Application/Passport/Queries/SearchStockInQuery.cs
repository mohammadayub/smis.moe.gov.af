using App.Application.Passport.Models;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Passport.Queries
{
    public class SearchStockInQuery : IRequest<List<StockInModel>>
    {
        public int? ID { get; set; }
        public int? ToUserID { get; set; }
        public int? PassportTypeID { get; set; }
    }

    public class SearchStockInQueryHandler : IRequestHandler<SearchStockInQuery, List<StockInModel>>
    {
        private AppDbContext Context { get; }
        private ICurrentUser CurrentUser { get; set; }
        public SearchStockInQueryHandler(ICurrentUser currentUser,AppDbContext dbContext)
        {
            Context = dbContext;
            CurrentUser = currentUser;
        }
        public async Task<List<StockInModel>> Handle(SearchStockInQuery request, CancellationToken cancellationToken)
        {
            var query = Context.StockIns.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ToUserID.HasValue)
            {
                query = query.Where(e => e.ToUserId == request.ToUserID);
            }
            if (request.PassportTypeID.HasValue)
            {
                query = query.Where(e => e.PassportTypeId == request.PassportTypeID);
            }

            return await  (
                from a in query
                join s in Context.SystemStatus on new { TypeId = a.StatusId,StatusType = StatusTypes.PassportStock } equals new { s.TypeId ,s.StatusType }
                select new StockInModel
                {
                    Id = a.Id,
                    PassportTypeId = a.PassportTypeId,
                    PassportDurationId = a.PassportDurationId,
                    PassportCount = a.PassportCount,
                    StartSerial = a.StartSerial,
                    EndSerial = a.EndSerial,
                    StatusId = a.StatusId,
                    ToUserId = a.ToUserId,
                    UsedCount = a.UsedCount,
                    PassportType = a.PassportType.Name,
                    PassportDuration = String.Concat( a.PassportDuration.Months," ماه"),
                    Status = s.Title
                }).ToListAsync();
            throw new NotImplementedException();
        }
    }
}
