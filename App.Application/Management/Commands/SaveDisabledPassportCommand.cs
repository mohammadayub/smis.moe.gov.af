using App.Application.Management.Models;
using App.Application.Management.Queries;
using App.Domain.Entity.pas;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Commands
{
    public class SaveDisabledPassportCommand : IRequest<List<SearchedProfileModel>>
    {
        public int? Id { get; set; }
        public int ApplicationStatusID { get; set; }
        public int DisabledReasonID { get; set; }
        public string Comment { get; set; }
    }

    public class SaveDisabledPassportCommandHandler : IRequestHandler<SaveDisabledPassportCommand, List<SearchedProfileModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveDisabledPassportCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchedProfileModel>> Handle(SaveDisabledPassportCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            if (!request.Id.HasValue)
            {
                throw new BusinessRulesException("ابتدا پاسپورت را پیدا کنید!");
            }
            var app = await Context.PassportApplications.Where(e => e.Id == request.Id).SingleAsync();
            if(app.StatusId != ApplicationStatus.Active)
            {
                throw new BusinessRulesException("این پاسپورت فعال نمی باشد!");
            }

            var print = Context.PassportPrints.Where(e => e.PrintQueue.ApplicationId == app.Id && e.StatusId == PassportPrintStatus.Printed).Single();
            var passport = Context.Passports.Where(e => e.Id == print.PassportId).Single();

            passport.StatusId = PassportStatus.InActive;
            app.StatusId = ApplicationStatus.Disabled;

            var DisabledPassport = new DisabledPassport
            {
                ApplicationId = app.Id,
                DisabledReasonId = request.DisabledReasonID,
                Comment = request.Comment,
                CreatedBy = UserID,
                CreatedOn = DateTime.Now
            };

            Context.DisabledPassports.Add(DisabledPassport);
            await Context.SaveChangesAsync();
            return await Mediator.Send(new SearchPrintedPassportsQuery { ID = request.Id });
        }
    }
}
