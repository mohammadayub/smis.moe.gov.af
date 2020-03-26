using App.Application.Registration.Models;
using App.Application.Registration.Queries;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Commands
{
    public class SaveCriminalRecordCommad : IRequest<List<CriminalRecordModel>>
    {
        public int? Id { get; set; }
        public int ProfileId { get; set; }
        public int CrimeTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }

    public class SaveCriminalRecordCommadHandler : IRequestHandler<SaveCriminalRecordCommad, List<CriminalRecordModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveCriminalRecordCommadHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<CriminalRecordModel>> Handle(SaveCriminalRecordCommad request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var cur = request.Id.HasValue ? Context.CriminalRecords.Where(e => e.Id == request.Id).Single() : new CriminalRecord();

            cur.ProfileId = request.ProfileId;
            cur.CrimeTypeId = request.CrimeTypeId;
            cur.Date = request.Date;
            cur.Description = request.Description;


            if (request.Id.HasValue)
            {
                cur.ModifiedBy = UserID;
                cur.ModifiedOn = DateTime.Now;
            }
            else
            {
                cur.CreatedBy = UserID;
                cur.CreatedOn = DateTime.Now;
                Context.CriminalRecords.Add(cur);
            }
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchCriminalRecordQuery { ID = cur.Id });
        }
    }
}