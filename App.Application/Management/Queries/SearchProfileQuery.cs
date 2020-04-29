using App.Application.Management.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Enums;
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

namespace App.Application.Management.Queries
{
    public class SearchProfileQuery : IRequest<List<SearchedProfileModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
        public string PassportNumber { get; set; }
        public string ProfileCode { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
    }

    public class SearchProfileQueryHandler : IRequestHandler<SearchProfileQuery, List<SearchedProfileModel>>
    {
        private AppDbContext Context { get; }
        private ICurrentUser CurrentUser { get; }
        public SearchProfileQueryHandler(AppDbContext dbContext,ICurrentUser currentUser)
        {
            Context = dbContext;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchedProfileModel>> Handle(SearchProfileQuery request, CancellationToken cancellationToken)
        {
            var UserId = await CurrentUser.GetUserId();

            var query = Context.PassportPrints.Where(e => e.StatusId == PassportPrintStatus.Printed)
                .Where(e => e.PrintQueue.Application.CurProcessId == SystemProcess.Close && e.PrintQueue.Application.StatusId == ApplicationStatus.Active)
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.PrintQueue.ApplicationId == request.ID);
            }
            else
            {
                //query = query.Where(e => e.PrintQueue.Application.CurProcessId == SystemProcess.Close && e.PrintQueue.Application.StatusId == ApplicationStatus.Active);

                if (!String.IsNullOrEmpty(request.Code))
                {
                    query = query.Where(e => e.PrintQueue.Application.Code == request.Code);
                }
                if (!String.IsNullOrEmpty(request.PassportNumber))
                {
                    query = query.Where(e => e.Passport.PassportNumber == request.PassportNumber);
                }
                if (!String.IsNullOrEmpty(request.ProfileCode))
                {
                    query = query.Where(e => e.PrintQueue.Application.Profile.Code == request.ProfileCode);
                }
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.Name, String.Concat("%", request.Name, "%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.FamilyName, String.Concat("%", request.FamilyName, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.FatherName, String.Concat("%", request.FatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.GrandFatherName, String.Concat("%", request.GrandFatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.NameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.NameEn, String.Concat("%", request.NameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.FamilyNameEn, String.Concat("%", request.FamilyNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.FatherNameEn, String.Concat("%", request.FatherNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.PrintQueue.Application.ActiveBioData.GrandFatherNameEn, String.Concat("%", request.GrandFatherNameEn, "%")));
                }
            }

            var list = await (from p in query
                              join bd in Context.BioDatas on p.PrintQueue.Application.ActiveBioDataId equals bd.Id

                              select new SearchedProfileModel
                              {
                                  Id = p.PrintQueue.ApplicationId,
                                  ApplicationID = p.PrintQueue.ApplicationId,
                                  ProfileID = p.PrintQueue.Application.ProfileId,
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
                                  DoBText = PersianDate.GetFormatedString(bd.DateOfBirth),
                                  DobShamsi = PersianDate.ToPersianDate(bd.DateOfBirth),
                                  Email = bd.Email,
                                  PhoneNumber = bd.PhoneNumber,

                                  PassportNumber = p.Passport.PassportNumber,

                                  PhotoPath = p.PrintQueue.Application.PhotoPath,
                                  SignaturePath = p.PrintQueue.Application.SignaturePath,

                                  PassportTypeID = p.PrintQueue.Application.PassportTypeId,
                                  PassportDurationID = p.PrintQueue.Application.PassportDurationId,
                                  ApplicationStatusID = p.PrintQueue.Application.StatusId,

                                  Code = p.PrintQueue.Application.Code,
                                  ProfileCode = p.PrintQueue.Application.Profile.Code,
                                  Height = p.PrintQueue.Application.Profile.Height,
                                  OtherDetail = p.PrintQueue.Application.Profile.OtherDetail,
                                  OtherNationalityId = p.PrintQueue.Application.Profile.OtherNationalityId,
                                  BirthCountryId = p.PrintQueue.Application.Profile.BirthCountryId,
                                  BirthCountryText = p.PrintQueue.Application.Profile.BirthCountry.Title,
                                  BirthProvinceId = p.PrintQueue.Application.Profile.BirthProvinceId,
                                  BirthProvinceText = p.PrintQueue.Application.Profile.BirthProvince.TitleEn,
                                  ResidenceCountryId = p.PrintQueue.Application.Profile.ResidenceCountryId,
                                  ResidenceCountryText = p.PrintQueue.Application.Profile.ResidenceCountry.Title,
                                  GenderId = p.PrintQueue.Application.Profile.GenderId,
                                  GenderText = p.PrintQueue.Application.Profile.Gender.Name,
                                  HairColorId = p.PrintQueue.Application.Profile.HairColorId,
                                  EyeColorId = p.PrintQueue.Application.Profile.EyeColorId,
                                  MaritalStatusId = p.PrintQueue.Application.Profile.MaritalStatusId,
                                  TitleId = p.PrintQueue.Application.Profile.TitleId,
                                  DocumentTypeId = p.PrintQueue.Application.Profile.DocumentTypeId,
                                  NID = p.PrintQueue.Application.Profile.NationalId,
                                  NIDText = NationalIDReader.ConvertJSONToString(p.PrintQueue.Application.Profile.NationalId, p.PrintQueue.Application.Profile.DocumentType.Name)
                              }
                        ).ToListAsync();

            return list;
        }
    }
}
