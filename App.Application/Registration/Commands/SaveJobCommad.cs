using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Registration.Models;
using App.Application.Registration.Queries;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;

namespace App.Application.Registration.Commands
{
    public class SaveJobCommad : IRequest<List<SearchJobModel>>
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public int OccupationId { get; set; }
        public string Employer { get; set; }
        public string EmployerAddress { get; set; }
        public string PrevEmployer { get; set; }
        public string PrevEmployerAddress { get; set; }
        public int ProfileId { get; set; }

    }

    public class SaveJobCommadHandler : IRequestHandler<SaveJobCommad, List<SearchJobModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveJobCommadHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchJobModel>> Handle(SaveJobCommad request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var cad = new Job
            {
                ProfileId = request.ProfileId,
                OccupationId = request.OccupationId,
                OrganizationId = request.OrganizationId,
                Employer = request.Employer,
                EmployerAddress = request.EmployerAddress,
                PrevEmployer = request.PrevEmployer,
                PrevEmployerAddress = request.PrevEmployerAddress
            };
            if (request.Id.HasValue)
            {
                Job cur = Context.Jobs.Where(e => e.Id == request.Id).Single();
                if (cur.OrganizationId != cad.OrganizationId || cur.OccupationId != cad.OccupationId )
                {
                    cad.CreatedBy = UserID;
                    cad.CreatedOn = DateTime.Now;
                    cad.StatusId = 1;
                    cur.StatusId = 0;
                    Context.Jobs.Add(cad);
                }
                else
                {
                    cur.Employer = cad.Employer;
                    cur.EmployerAddress = cad.EmployerAddress;
                    cur.PrevEmployer = cad.PrevEmployer;
                    cur.PrevEmployerAddress = cad.PrevEmployerAddress;
                    cur.ModifiedBy = UserID;
                    cur.ModifiedOn = DateTime.Now;
                    cad.Id = cur.Id;
                }
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchJobQuery { ID = cad.Id });
            }
            else
            {
                cad.StatusId = 1;
                cad.CreatedBy = UserID;
                cad.CreatedOn = DateTime.Now;
                Context.Jobs.Add(cad);
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchJobQuery { ID = cad.Id });
            }
        }
    }
}
