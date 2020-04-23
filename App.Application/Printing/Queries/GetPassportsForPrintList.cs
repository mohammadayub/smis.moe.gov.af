using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Printing.Queries
{
    public class GetPassportsForPrintList:IRequest<List<PrintPassportInformation>>
    {
        public int PassportTypeID { get; set; }
        public int PassportDurationID { get; set; }
    }
    public class GetPassportsForPrintListHandler : IRequestHandler<GetPassportsForPrintList, List<PrintPassportInformation>>
    {
        private ICurrentUser CurrentUser { get; }
        private AppDbContext Context { get; }
        public GetPassportsForPrintListHandler(AppDbContext context,ICurrentUser currentUser)
        {
            CurrentUser = currentUser;
            Context = context;
        }
        public async Task<List<PrintPassportInformation>> Handle(GetPassportsForPrintList request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var query = Context.PassportPrints
                .Where(e => e.StatusId == PassportPrintStatus.Registered && e.CreatedBy == UserID)
                .Where(e => e.PrintQueue.Application.PassportTypeId == request.PassportTypeID)
                .Where(e => e.PrintQueue.Application.PassportDurationId == request.PassportDurationID);

            return await query.Select(e => new PrintPassportInformation { 
                ID = e.Id,
                ApplicationID = e.PrintQueue.ApplicationId,
                ProfileID = e.PrintQueue.Application.ProfileId,
                Code = e.PrintQueue.Application.Code,
                Name = e.PrintQueue.Application.ActiveBioData.Name,
                FamilyName = e.PrintQueue.Application.ActiveBioData.FamilyName,
                FatherName = e.PrintQueue.Application.ActiveBioData.FatherName,
                Gender = e.PrintQueue.Application.Profile.Gender.Name,
                PassportNumber = e.Passport.PassportNumber,
                PassportType = e.PrintQueue.Application.PassportType.Name,
                PassportDuration = String.Concat( e.PrintQueue.Application.PassportDuration.Months," ماه")
            }).ToListAsync();
        }
    }
}
