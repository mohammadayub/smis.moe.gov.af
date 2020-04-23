using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Common;
using Clean.Common.Dates;
using Clean.Common.Service;
using Clean.Common.Storage;
using Clean.Persistence.Identity;
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
    public class GetPassportPrintFullInformationQuery: IRequest<PassportPrintFullInformation>
    {
        public long ID { get; set; }
    }

    public class GetPassportPrintFullInformationHandler : IRequestHandler<GetPassportPrintFullInformationQuery, PassportPrintFullInformation>
    {
        private ICurrentUser CurrentUser { get; }
        private AppDbContext Context { get; }
        private AppIdentityDbContext IdentityDbContext { get; set; }
        public GetPassportPrintFullInformationHandler(AppDbContext dbContext,ICurrentUser currentUser, AppIdentityDbContext identityDbContext)
        {
            CurrentUser = currentUser;
            Context = dbContext;
            IdentityDbContext = identityDbContext;
        }
        public async Task<PassportPrintFullInformation> Handle(GetPassportPrintFullInformationQuery request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var query = Context.PassportPrints.Where(e => e.Id == request.ID && e.CreatedBy == UserID);
            var result = await query.Select(e => new PassportPrintFullInformation
            {
                ID = e.Id,
                ApplicationID = e.PrintQueue.ApplicationId,
                ApplicationCode = e.PrintQueue.Application.Code,
                ProfileID = e.PrintQueue.Application.ProfileId,
                ProfileCode = e.PrintQueue.Application.Profile.Code,
                Name = e.PrintQueue.Application.ActiveBioData.Name,
                NameEn = e.PrintQueue.Application.ActiveBioData.NameEn,
                FamilyName = e.PrintQueue.Application.ActiveBioData.FamilyName,
                FamilyNameEn = e.PrintQueue.Application.ActiveBioData.FamilyNameEn,
                DateOfBirthFull = e.PrintQueue.Application.ActiveBioData.DateOfBirth,
                DateOfBirth = e.PrintQueue.Application.ActiveBioData.DateOfBirth.ToString("dd MMM yyyy"),
                DateOfBirthShamsi = PersianDate.ToPassportFormat(e.PrintQueue.Application.ActiveBioData.DateOfBirth),
                IssueDate = e.PrintedDate.ToString("dd MMM yyyy"),
                ExpiryDate = e.ValidTo.ToString("dd MMM yyyy"),
                ExpireDateFull = e.ValidTo,
                IssueDateShamsi = PersianDate.ToPassportFormat(e.PrintedDate),
                ExpiryDateShamsi = PersianDate.ToPassportFormat(e.ValidTo),
                PassportNumber = e.Passport.PassportNumber,
                PassportType = e.PrintQueue.Application.PassportType.Code,
                BirthCountry = e.PrintQueue.Application.Profile.BirthCountry.Title,
                BirthCountryEN = e.PrintQueue.Application.Profile.BirthCountry.TitleEn,
                Gender = e.PrintQueue.Application.Profile.Gender.Code,
                Height = e.PrintQueue.Application.Profile.Height,
                NIDSerial = NationalIDReader.GetTazkiraNumber(e.PrintQueue.Application.Profile.NationalId, e.PrintQueue.Application.Profile.DocumentType.Name),
                CountryCode = AppConfig.NationalCode,
                Occupation = e.PrintQueue.Application.ActiveJob.Occupation.Title,
                OccupationEn = e.PrintQueue.Application.ActiveJob.Occupation.TitleEn,
                PersonPhoto = e.PrintQueue.Application.PhotoPath,
                PersonSignature = e.PrintQueue.Application.SignaturePath
            }).FirstOrDefaultAsync();

            if(result != null)
            {
                var office = await IdentityDbContext.Users.Where(e => e.Id == UserID).Select(e => e.Office).SingleAsync();
                result.OfficeName = office.Title;
                result.OfficeNameEn = office.TitleEn;

                result.MRZLineOne = MRZHelper.GenerateFirstLine(result.NameEn, result.FamilyNameEn, result.PassportType);
                result.MRZLineTwo = MRZHelper.GenerateSecondLine(result.PassportNumber, result.DateOfBirthFull, result.Gender, result.ExpireDateFull);

                result.PersonPhoto = await FileStorage.GetFileContent(AppConfig.ImagesPath, result.PersonPhoto);
                result.PersonSignature = await FileStorage.GetFileContent(AppConfig.SignaturesPath, result.PersonSignature);
            }

            return result;
        }



    }
}
