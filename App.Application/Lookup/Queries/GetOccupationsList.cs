using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetOccupationsList : IRequest<List<OccupationModel>>
    {
        public int? ID { get; set; }
        public int? OrganizationID { get; set; }
    }

    public class GetOccupationsListHandler : IRequestHandler<GetOccupationsList, List<OccupationModel>>
    {
        private AppDbContext Context { get; set; }
        public GetOccupationsListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<OccupationModel>> Handle(GetOccupationsList request, CancellationToken cancellationToken)
        {
            var query = Context.Occupations.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.OrganizationID.HasValue)
            {
                query = query.Where(e => e.OrganizationId == request.OrganizationID);
            }

            return await query.Select(e => new OccupationModel
            {
                Id = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                OrganizationId = e.OrganizationId,
                Organization = e.Organization.Name
            }).ToListAsync();
        }
    }
}
