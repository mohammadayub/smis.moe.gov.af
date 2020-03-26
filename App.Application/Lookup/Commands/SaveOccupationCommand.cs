using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Persistence.Context;
using Clean.Common.Exceptions;
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
    public class SaveOccupationCommand : IRequest<List<OccupationModel>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int OrganizationId { get; set; }

    }

    public class SaveOccupationCommandHandler : IRequestHandler<SaveOccupationCommand, List<OccupationModel>>
    {
        public AppDbContext Context { get; set; }
        public IMediator Mediator { get; set; }
        public ICurrentUser CurrentUser { get; set; }
        public SaveOccupationCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<OccupationModel>> Handle(SaveOccupationCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();

            var cur = request.Id.HasValue ? Context.Occupations.Where(e => e.Id == request.Id).Single() : new Domain.Entity.look.Occupation();

            cur.Title = request.Title;
            cur.TitleEn = request.TitleEn;
            cur.OrganizationId = request.OrganizationId;
            
            if (request.Id.HasValue)
            {
                cur.CreatedOn = DateTime.Now;
                if(!(await CurrentUser.IsSuperAdmin()).Value)
                {
                    if(cur.CreatedBy != UserID)
                    {
                        throw new BusinessRulesException("این وظیفه توسط شخص دیگری اضافه شده است و قابل تغییر نمی باشد!");
                    }
                }
            }
            else
            {
                cur.CreatedOn = DateTime.Now;
                cur.CreatedBy = UserID;
                Context.Occupations.Add(cur);
            }

            await Context.SaveChangesAsync();
            return await Mediator.Send(new GetOccupationsList { ID = cur.Id });
        }
    }
}
