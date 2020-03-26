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
    public class SearchCriminalRecordQuery:IRequest<List<CriminalRecordModel>>
    {
        public int? ID { get; set; }
        public int? ProfileID { get; set; }
    }

    public class SearchCriminalRecordQueryHandler : IRequestHandler<SearchCriminalRecordQuery, List<CriminalRecordModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchCriminalRecordQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<CriminalRecordModel>> Handle(SearchCriminalRecordQuery request, CancellationToken cancellationToken)
        {
            var query = Context.CriminalRecords.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProfileID.HasValue)
            {
                query = query.Where(e => e.ProfileId == request.ProfileID);
            }

            return await query.Select(e => new CriminalRecordModel
            {
                Id = e.Id,
                ProfileId = e.ProfileId,
                CrimeTypeId = e.CrimeTypeId,
                CrimeType = e.CrimeType.Title,
                Date = e.Date.ToString("yyyy-MM-dd"),
                DateShamsi = PersianDate.ToPersianDate(e.Date),
                Description = e.Description
            }).ToListAsync();

        }
    }

}
