using App.Application.Registration.Models;
using App.Persistence.Context;
using Clean.Persistence.Identity;
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
    public class GetApplicationOfficeQuery : IRequest<SearchOfficeModel>
    {
        public int ID { get; set; }

    }

    public class GetApplicationOfficeQueryHandler : IRequestHandler<GetApplicationOfficeQuery, SearchOfficeModel>
    {
        private AppDbContext Context { get; }
        private AppIdentityDbContext IdentityDbContext { get; }
        public GetApplicationOfficeQueryHandler(AppDbContext context, AppIdentityDbContext identityDbContext)
        {
            Context = context;
            IdentityDbContext = identityDbContext;
        }
        public async Task<SearchOfficeModel> Handle(GetApplicationOfficeQuery request, CancellationToken cancellationToken)
        {
            var query = Context.PassportApplications.Where(e => e.Id == request.ID).Single();

            var user = IdentityDbContext.Users.Where(e => e.Id == query.CreatedBy).Single();

            var office = await Context.Offices.Where(e => e.Id == user.OfficeID)
                .Select(e => new SearchOfficeModel
                {
                    ID = e.Id,
                    Name = e.Title,
                    NameEn = e.TitleEn,
                    Code = e.Code
                }).SingleAsync();

            return office;
        }
    }
}