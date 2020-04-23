using App.Application.Printing.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Printing.Queries
{
    public class GetPassportTypeAndDurationQuery : IRequest<List<PassportTypesModel>>
    {
        public int? ID { get; set; }
    }
    public class SearchPassportTypeQueryHandler : IRequestHandler<GetPassportTypeAndDurationQuery, List<PassportTypesModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchPassportTypeQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PassportTypesModel>> Handle(GetPassportTypeAndDurationQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportTypes
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            return await query.Select(i => new PassportTypesModel
            {
                ID = i.Id,
                Title = i.Name,
                Code = i.Code,
                PassportDurations = i.PassportDuration.Select(e => new PassportDurationModel
                {
                    ID = e.Id,
                    Name = String.Concat(e.Months , " ماه"),
                    Months = e.Months
                }).ToList()
            }).ToListAsync();
        }
    }

}
