using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
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
    public class SaveOfficeCommand : IRequest<List<SearchOfficeModel>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CurrencyId { get; set; }
        public int OrganizationId { get; set; }
        public int OfficeTypeId { get; set; }

    }

    public class SaveOfficeCommandHandler : IRequestHandler<SaveOfficeCommand, List<SearchOfficeModel>>
    {
        public AppDbContext Context { get; set; }
        public IMediator Mediator { get; set; }
        public ICurrentUser CurrentUser { get; set; }
        public SaveOfficeCommandHandler(AppDbContext context, IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchOfficeModel>> Handle(SaveOfficeCommand request, CancellationToken cancellationToken)
        {
            var cur = request.Id.HasValue ? Context.Offices.Where(e => e.Id == request.Id).Single() : new Domain.Entity.look.Office();

            cur.Title = request.Title;
            cur.TitleEn = request.TitleEn;
            cur.Code = request.Code;
            cur.CurrencyId = request.CurrencyId;
            cur.CountryId = request.CountryId;
            cur.ProvinceId = request.ProvinceId;
            cur.OrganizationId = request.OrganizationId;
            cur.OfficeTypeId = request.OfficeTypeId;

            if (request.Id.HasValue)
            {
                cur.ModifiedOn = DateTime.Now;
                cur.ModifiedBy = await CurrentUser.GetUserId();
            }
            else
            {
                cur.CreatedBy = await CurrentUser.GetUserId();
                cur.CreatedOn = DateTime.Now;
                Context.Offices.Add(cur);
            }

            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetOfficesQuery { ID = cur.Id });
        }
    }
}
