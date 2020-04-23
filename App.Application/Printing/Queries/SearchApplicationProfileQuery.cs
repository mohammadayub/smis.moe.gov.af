using App.Application.Printing.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Service;
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
    public class SearchApplicationProfileQuery : IRequest<ApplicationProfileModel>
    {
        public int ApplicationID { get; set; }
    }

    public class SearchApplicationProfileQueryHandler : IRequestHandler<SearchApplicationProfileQuery, ApplicationProfileModel>
    {
        private AppDbContext Context { get; }
        private ICurrentUser CurrentUser { get; }
        public SearchApplicationProfileQueryHandler(AppDbContext dbContext,ICurrentUser currentUser)
        {
            Context = dbContext;
            CurrentUser = currentUser;
        }
        public async Task<ApplicationProfileModel> Handle(SearchApplicationProfileQuery request, CancellationToken cancellationToken)
        {
            var UserId = await CurrentUser.GetUserId();
            

            var query = Context.PassportApplications.Where(e => e.Id == request.ApplicationID)
                .AsQueryable();


            var list = await (from p in query
                              join bd in Context.BioDatas on p.ActiveBioDataId equals bd.Id

                              select new ApplicationProfileModel
                              {
                                  Id = p.Id,
                                  ProfileID = p.ProfileId,
                                  BId = bd.Id,
                                  Name = bd.Name,
                                  NameEn = bd.NameEn,
                                  FamilyName = bd.FamilyName,
                                  FamilyNameEn = bd.FamilyNameEn,
                                  FatherName = bd.FatherName,
                                  FatherNameEn = bd.FatherNameEn,
                                  GrandFatherName = bd.GrandFatherName,
                                  GrandFatherNameEn = bd.GrandFatherNameEn,
                                  DateOfBirth = bd.DateOfBirth.ToString("yyyy-MM-dd"),
                                  DateOfBirthFull = bd.DateOfBirth,
                                  DateOfBirthPassport = bd.DateOfBirth.ToString("dd MMM yyyy"),
                                  DobShamsi = PersianDate.ToPersianDate(bd.DateOfBirth),
                                  DobShamsiPassport = PersianDate.ToPassportFormat(bd.DateOfBirth),
                                  Email = bd.Email,
                                  PhoneNumber = bd.PhoneNumber,

                                  Code = p.Code,
                                  ProfileCode = p.Profile.Code,
                                  Height = p.Profile.Height,
                                  OtherDetail = p.Profile.OtherDetail,
                                  OtherNationalityId = p.Profile.OtherNationalityId,
                                  BirthCountryId = p.Profile.BirthCountryId,
                                  BirthCountryText = p.Profile.BirthCountry.Title,
                                  BirthCountryTextEn = p.Profile.BirthCountry.TitleEn,
                                  BirthProvinceId = p.Profile.BirthProvinceId,
                                  BirthProvinceText = p.Profile.BirthProvince.TitleEn,
                                  ResidenceCountryId = p.Profile.ResidenceCountryId,
                                  ResidenceCountryText = p.Profile.ResidenceCountry.Title,
                                  GenderId = p.Profile.GenderId,
                                  GenderText = p.Profile.Gender.Name,
                                  GenderEn = p.Profile.Gender.Code,
                                  HairColorId = p.Profile.HairColorId,
                                  EyeColorId = p.Profile.EyeColorId,
                                  MaritalStatusId = p.Profile.MaritalStatusId,
                                  TitleId = p.Profile.TitleId,
                                  DocumentTypeId = p.Profile.DocumentTypeId,
                                  NID = p.Profile.NationalId,
                                  NIDText = NationalIDReader.ConvertJSONToString(p.Profile.NationalId, p.Profile.DocumentType.Name),
                                  NIDSerial = NationalIDReader.GetTazkiraNumber(p.Profile.NationalId,p.Profile.DocumentType.Name)
                              }
                        ).FirstOrDefaultAsync();

            return list;
        }
    }
}
