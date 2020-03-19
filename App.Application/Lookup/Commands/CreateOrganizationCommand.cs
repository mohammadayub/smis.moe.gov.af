using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Commands
{
    public class CreateOrganizationCommand : IRequest<List<SearchOrganizationModel>>
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public short OrganizationTypeID { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, List<SearchOrganizationModel>>
    {
        public AppDbContext Context { get; set; }
        public IMediator Mediator { get; set; }
        public CreateOrganizationCommandHandler(AppDbContext context, IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
        }
        public async Task<List<SearchOrganizationModel>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {


            List<SearchOrganizationModel> result = new List<SearchOrganizationModel>();
            bool IsUpdate = request.Id.HasValue && request.Id != default(int) ? true : false;

            var o = IsUpdate ? (await Context.Organizations.Where(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken)) : new Domain.Entity.look.Organization();
            o.Code = request.Code;
            o.Name = request.Name;
            o.Dari = request.Dari;
            o.Pashto = request.Pashto;
            o.OrganizationTypeId = request.OrganizationTypeID;

            if (IsUpdate)
            {
                await Context.SaveChangesAsync(cancellationToken);
                result = await Mediator.Send(new GetOrganiztionQuery() { Id = o.Id });
            }
            else
            {
                Context.Organizations.Add(o);
                await Context.SaveChangesAsync(cancellationToken);
            }


            result = await Mediator.Send(new GetOrganiztionQuery() { Id = o.Id });
            return result;
        }
    }
}
