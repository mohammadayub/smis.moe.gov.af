using App.Application.Passport.Models;
using App.Persistence.Context;
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
    public class SearchPassportDurationQuery : IRequest<List<PassportDurationModel>>
    {
        public int? ID { get; set; }
        public int? PassportTypeID { get; set; }
    }
    public class SearchPassportDurationQueryHandler : IRequestHandler<SearchPassportDurationQuery, List<PassportDurationModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchPassportDurationQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PassportDurationModel>> Handle(SearchPassportDurationQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportDurations
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            if (request.PassportTypeID.HasValue)
            {
                query = query.Where(e => e.PassportTypeId == request.PassportTypeID);
            }

            return await query.Select(i => new PassportDurationModel
            {
                ID = i.Id,
                PassportTypeID = i.PassportTypeId,
                Months = i.Months,
                PassportType = i.PassportType.Name
            }).ToListAsync();
        }
    }

}
