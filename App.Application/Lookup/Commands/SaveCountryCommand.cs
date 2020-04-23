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
    public class SaveCountryCommand: IRequest<List<CountryModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string TitleLocal { get; set; }
    }
    public class SaveCountryCommandHandler : IRequestHandler<SaveCountryCommand, List<CountryModel>>
    {
        private AppDbContext Context {get;set;}
        private IMediator Mediator { get; set; }
        public SaveCountryCommandHandler(AppDbContext context,IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
        }
        public async Task<List<CountryModel>> Handle(SaveCountryCommand request, CancellationToken cancellationToken)
        {
            var country = request.ID.HasValue ? Context.Countries.Where(e => e.Id == request.ID).Single() : new Clean.Domain.Entity.look.Country();
            country.Code = request.Code;
            country.TitleEn = request.Title;
            country.Title = request.TitleLocal;
            if (!request.ID.HasValue)
            {
                Context.Countries.Add(country);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetCountryList { ID = country.Id });
        }
    }
}
