using App.Application.Printing.Models;
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

namespace App.Application.Printing.Queries
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
            
            var query = Context.PrintQueues.Where(e => e.IsProcessed == false && e.UserId == UserId)
                .Where(e => e.Application.CurProcessId == SystemProcess.Print)
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (!String.IsNullOrEmpty(request.Code))
                {
                    query = query.Where(e => e.Application.Code == request.Code);
                }
                if (!String.IsNullOrEmpty(request.ProfileCode))
                {
                    query = query.Where(e => e.Application.Profile.Code == request.ProfileCode);
                }
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => EF.Functions.ILike( e.Application.ActiveBioData.Name, String.Concat("%", request.Name,"%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.FamilyName, String.Concat("%", request.FamilyName, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.FatherName, String.Concat("%", request.FatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.GrandFatherName, String.Concat("%", request.GrandFatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.NameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.NameEn, String.Concat("%", request.NameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FamilyNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.FamilyNameEn, String.Concat("%", request.FamilyNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.FatherNameEn, String.Concat("%", request.FatherNameEn, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherNameEn))
                {
                    query = query.Where(e => EF.Functions.ILike(e.Application.ActiveBioData.GrandFatherNameEn, String.Concat("%", request.GrandFatherNameEn, "%")));
                }
            }

            var list = await (from p in query
                              join bd in Context.BioDatas on p.Application.ActiveBioDataId equals bd.Id

                              select new SearchedProfileModel
                              {
                                  Id = p.Id,
                                  ApplicationID = p.ApplicationId,
                                  ProfileID = p.Application.ProfileId,
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

                                  Code = p.Application.Code,
                                  ProfileCode = p.Application.Profile.Code,
                                  Height = p.Application.Profile.Height,
                                  OtherDetail = p.Application.Profile.OtherDetail,
                                  OtherNationalityId = p.Application.Profile.OtherNationalityId,
                                  BirthCountryId = p.Application.Profile.BirthCountryId,
                                  BirthCountryText = p.Application.Profile.BirthCountry.Title,
                                  BirthProvinceId = p.Application.Profile.BirthProvinceId,
                                  BirthProvinceText = p.Application.Profile.BirthProvince.TitleEn,
                                  ResidenceCountryId = p.Application.Profile.ResidenceCountryId,
                                  ResidenceCountryText = p.Application.Profile.ResidenceCountry.Title,
                                  GenderId = p.Application.Profile.GenderId,
                                  GenderText = p.Application.Profile.Gender.Name,
                                  HairColorId = p.Application.Profile.HairColorId,
                                  EyeColorId = p.Application.Profile.EyeColorId,
                                  MaritalStatusId = p.Application.Profile.MaritalStatusId,
                                  TitleId = p.Application.Profile.TitleId,
                                  DocumentTypeId = p.Application.Profile.DocumentTypeId,
                                  NID = p.Application.Profile.NationalId,
                                  NIDText = NationalIDReader.ConvertJSONToString(p.Application.Profile.NationalId, p.Application.Profile.DocumentType.Name)
                              }
                        ).ToListAsync();

            return list;
        }
    }
}
