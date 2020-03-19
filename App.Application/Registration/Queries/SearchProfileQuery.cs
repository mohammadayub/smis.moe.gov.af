using App.Application.Registration.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Queries
{
    public class SearchProfileQuery : IRequest<List<SearchedProfileModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
    }

    public class SearchProfileQueryHandler : IRequestHandler<SearchProfileQuery, List<SearchedProfileModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchProfileQueryHandler(AppDbContext dbContext)
        {
            Context = dbContext;
        }
        public async Task<List<SearchedProfileModel>> Handle(SearchProfileQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Profiles
                .Include(e => e.Gender)
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (!String.IsNullOrEmpty(request.Code))
                {
                    query = query.Where(e => e.Code == request.Code);
                }
            }

            var list = await (from p in query
                              join bd in Context.BioDatas on new { StatusId = 1, ProfileId = p.Id } equals new { bd.StatusId, bd.ProfileId }

                              select new SearchedProfileModel
                              {
                                  Id = p.Id,
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
                                  Height = p.Height,
                                  OtherDetail = p.OtherDetail,
                                  OtherNationalityId = p.OtherNationalityId,
                                  BirthCountryId = p.BirthCountryId,
                                  BirthCountryText = p.BirthCountry.Title,
                                  BirthProvinceId = p.BirthProvinceId,
                                  BirthProvinceText = p.BirthProvince.TitleEn,
                                  ResidenceCountryId = p.ResidenceCountryId,
                                  ResidenceCountryText = p.ResidenceCountry.Title,
                                  GenderId = p.GenderId,
                                  GenderText = p.Gender.Name,
                                  HairColorId = p.HairColorId,
                                  EyeColorId = p.EyeColorId,
                                  MaritalStatusId = p.MaritalStatusId,
                                  TitleId = p.TitleId,
                                  DocumentTypeId = p.DocumentTypeId,
                                  NID = p.NationalId,
                                  NIDText = NationalIDReader.ConvertJSONToString(p.NationalId, p.DocumentType.Name)
                              }
                        ).ToListAsync();

            return list;
        }
    }
}
