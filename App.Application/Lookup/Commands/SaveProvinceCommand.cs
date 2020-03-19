using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Commands
{
    public class SaveProvinceCommand : IRequest<List<ProvinceModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int CountryID { get; set; }
    }
    public class SaveProvinceCommandHandler : IRequestHandler<SaveProvinceCommand, List<ProvinceModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        public SaveProvinceCommandHandler(AppDbContext context, IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
        }
        public async Task<List<ProvinceModel>> Handle(SaveProvinceCommand request, CancellationToken cancellationToken)
        {
            var country = request.ID.HasValue ? Context.Provinces.Where(e => e.Id == request.ID).Single() : new Clean.Domain.Entity.look.Province();
            country.Code = request.Code;
            country.Title = request.Title;
            country.TitleEn = request.TitleEn;
            country.CountryId = request.CountryID;
            if (!request.ID.HasValue)
            {
                Context.Provinces.Add(country);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetProvinceList { ID = country.Id });
        }
    }
}
