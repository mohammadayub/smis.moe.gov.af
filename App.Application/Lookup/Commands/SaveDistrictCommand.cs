using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Commands
{
    public class SaveDistrictCommand : IRequest<List<DistrictModel>>
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int ProvinceID { get; set; }
    }
    public class SaveDistrictCommandHandler : IRequestHandler<SaveDistrictCommand, List<DistrictModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        public SaveDistrictCommandHandler(AppDbContext context, IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<DistrictModel>> Handle(SaveDistrictCommand request, CancellationToken cancellationToken)
        {
            var country = request.ID.HasValue ? Context.Districts.Where(e => e.Id == request.ID).Single() : new Clean.Domain.Entity.look.District();
            country.Title = request.Title;
            country.TitleEn = request.TitleEn;
            country.ProvinceId = request.ProvinceID;
            country.CreatedBy = await CurrentUser.GetUserId();
            country.CreatedOn = DateTime.Now;

            if (!request.ID.HasValue)
            {
                Context.Districts.Add(country);
            }

            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetDistrictList { ID = country.Id });
        }
    }
}
