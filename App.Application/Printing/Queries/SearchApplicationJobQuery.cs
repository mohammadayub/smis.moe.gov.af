using App.Application.Printing.Models;
using App.Application.Registration.Models;
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
    public class SearchApplicationJobQuery : IRequest<ApplicationJobModel>
    {
        public int ApplicationID { get; set; }
    }

    public class SearchApplicationJobQueryHandler : IRequestHandler<SearchApplicationJobQuery, ApplicationJobModel>
    {
        private AppDbContext Context { get; set; }
        public SearchApplicationJobQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<ApplicationJobModel> Handle(SearchApplicationJobQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportApplications.Where(e => e.Id == request.ApplicationID)
                .AsQueryable();
            


            return await query.Select(e => new ApplicationJobModel
            {
                Id = e.ActiveJob.Id,
                OrganizationId = e.ActiveJob.OrganizationId,
                OccupationId = e.ActiveJob.OccupationId,
                ProfileId = e.ProfileId,
                Employer = e.ActiveJob.Employer,
                EmployerAddress = e.ActiveJob.EmployerAddress,
                PrevEmployer = e.ActiveJob.PrevEmployer,
                PrevEmployerAddress = e.ActiveJob.PrevEmployerAddress,
                Organization = e.ActiveJob.Organization.Name,
                Occupation = e.ActiveJob.Occupation.Title,
                OccupationEn = e.ActiveJob.Occupation.TitleEn
            }).SingleOrDefaultAsync();
        }
    }
}
