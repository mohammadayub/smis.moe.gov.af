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
    public class SearchPassportTypeQuery : IRequest<List<PassportTypeModel>>
    {
        public int? ID { get; set; }
    }
    public class SearchPassportTypeQueryHandler : IRequestHandler<SearchPassportTypeQuery, List<PassportTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchPassportTypeQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<PassportTypeModel>> Handle(SearchPassportTypeQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportTypes
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            return await query.Select(i => new PassportTypeModel
            {
                Id = i.Id,
                Name = i.Name,
                Code = i.Code,
                SerialLength = i.SerialLength
            }).ToListAsync();
        }
    }

}
