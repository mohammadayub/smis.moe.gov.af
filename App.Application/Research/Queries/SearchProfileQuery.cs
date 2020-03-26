using App.Application.Research.Models;
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

namespace App.Application.Research.Queries
{
    public class SearchProfileQuery : IRequest<List<SearchedProfileModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
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
            
            var query = Context.ResearchQueues.Where(e => e.UserId == UserId && e.Processed == false).Select(e => e.Application)
                .Where(e => e.CurProcessId == SystemProcess.ReasearchAndControl)
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.ProfileId == request.ID);
            }
            else
            {
                if (!String.IsNullOrEmpty(request.Code))
                {
                    query = query.Where(e => e.Code == request.Code);
                }
                if (!String.IsNullOrEmpty(request.ProfileCode))
                {
                    query = query.Where(e => e.Profile.Code == request.ProfileCode);
                }
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => EF.Functions.ILike( e.ActiveBioData.Name, String.Concat("%", request.Name,"%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.FamilyName, String.Concat("%", request.FamilyName, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.FatherName, String.Concat("%", request.FatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.GrandFatherName, String.Concat("%", request.GrandFatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.NameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.NameEn, String.Concat("%", request.NameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.FamilyNameEn, String.Concat("%", request.FamilyNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.FatherNameEn, String.Concat("%", request.FatherNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.ActiveBioData.GrandFatherNameEn, String.Concat("%", request.GrandFatherNameEn, "%")));
                }
            }

            var list = await (from p in query
                              join bd in Context.BioDatas on p.ActiveBioDataId equals bd.Id

                              select new SearchedProfileModel
                              {
                                  Id = p.ProfileId,
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

                                  Code = p.Code,
                                  ProfileCode = p.Profile.Code,
                                  Height = p.Profile.Height,
                                  OtherDetail = p.Profile.OtherDetail,
                                  OtherNationalityId = p.Profile.OtherNationalityId,
                                  BirthCountryId = p.Profile.BirthCountryId,
                                  BirthCountryText = p.Profile.BirthCountry.Title,
                                  BirthProvinceId = p.Profile.BirthProvinceId,
                                  BirthProvinceText = p.Profile.BirthProvince.TitleEn,
                                  ResidenceCountryId = p.Profile.ResidenceCountryId,
                                  ResidenceCountryText = p.Profile.ResidenceCountry.Title,
                                  GenderId = p.Profile.GenderId,
                                  GenderText = p.Profile.Gender.Name,
                                  HairColorId = p.Profile.HairColorId,
                                  EyeColorId = p.Profile.EyeColorId,
                                  MaritalStatusId = p.Profile.MaritalStatusId,
                                  TitleId = p.Profile.TitleId,
                                  DocumentTypeId = p.Profile.DocumentTypeId,
                                  NID = p.Profile.NationalId,
                                  NIDText = NationalIDReader.ConvertJSONToString(p.Profile.NationalId, p.Profile.DocumentType.Name)
                              }
                        ).ToListAsync();

            return list;
        }
    }
}
