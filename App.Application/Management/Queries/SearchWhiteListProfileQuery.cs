using App.Application.Management.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Enums;
using Clean.Common.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Queries
{
    public class SearchWhiteListProfileQuery : IRequest<List<SearchWhiteListProfileModel>>
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
    }


    public class SearchWhiteListProfileQueryHandler : IRequestHandler<SearchWhiteListProfileQuery, List<SearchWhiteListProfileModel>>
    {
        private AppDbContext Context { get; }
        public SearchWhiteListProfileQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchWhiteListProfileModel>> Handle(SearchWhiteListProfileQuery request, CancellationToken cancellationToken)
        {
            var query = Context.BlackLists
                .Where(e => e.StatusId == BlackListStatus.Active)
                .AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            else
            {
                if (!String.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(e => EF.Functions.ILike(e.BlackListProfile.Name, String.Concat("%", request.Name, "%")));
                }
                if (!String.IsNullOrEmpty(request.FatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.BlackListProfile.FatherName, String.Concat("%", request.FatherName, "%")));
                }
                if (!String.IsNullOrEmpty(request.GrandFatherName))
                {
                    query = query.Where(e => EF.Functions.ILike(e.BlackListProfile.GrandFatherName, String.Concat("%", request.GrandFatherName, "%")));
                }
            }


            return await (from e in query
                          select new SearchWhiteListProfileModel
                          {
                              Id = e.Id,
                              BlackListProfileId = e.Id,
                              Code = e.BlackListProfile.Code,

                              BlackListDate = PersianDate.ToPersianDate(e.BlackListDate),
                              BlackListReason = e.BlackListReason.Title,

                              TitleId = e.BlackListProfile.TitleId,
                              DocumentTypeId = e.BlackListProfile.DocumentTypeId,
                              NID = e.BlackListProfile.NationalId,
                              NIDText = NationalIDReader.ConvertJSONToString(e.BlackListProfile.NationalId, e.BlackListProfile.DocumentType.Name),

                              Name = e.BlackListProfile.Name,
                              NameEn = e.BlackListProfile.NameEn,
                              FamilyName = e.BlackListProfile.FamilyName,
                              FamilyNameEn = e.BlackListProfile.FamilyNameEn,
                              FatherName = e.BlackListProfile.FatherName,
                              FatherNameEn = e.BlackListProfile.FatherNameEn,
                              GrandFatherName = e.BlackListProfile.GrandFatherName,
                              GrandFatherNameEn = e.BlackListProfile.GrandFatherNameEn,
                              DateOfBirth = e.BlackListProfile.DateOfBirth.ToString("yyyy-MM-dd"),
                              DobShamsi = PersianDate.ToPersianDate(e.BlackListProfile.DateOfBirth),

                              GenderId = e.BlackListProfile.GenderId,
                              GenderText = e.BlackListProfile.Gender.Name,
                              MaritalStatusId = e.BlackListProfile.MaritalStatusId,
                              Height = e.BlackListProfile.Height,
                              HairColorId = e.BlackListProfile.HairColorId,
                              EyeColorId = e.BlackListProfile.EyeColorId,
                              OtherDetail = e.BlackListProfile.OtherDetail,
                              ResidenceCountryId = e.BlackListProfile.ResidenceCountryId,
                              ResidenceCountryText = e.BlackListProfile.ResidenceCountry.TitleEn,
                              BirthCountryId = e.BlackListProfile.BirthCountryId,
                              BirthCountryText = e.BlackListProfile.BirthCountry.Title,
                              BirthProvinceId = e.BlackListProfile.BirthProvinceId,
                              BirthProvinceText = e.BlackListProfile.BirthProvince.TitleEn,
                              OtherNationalityId = e.BlackListProfile.OtherNationalityId,

                              PhotoPath = e.BlackListProfile.PhotoPath

                          }).ToListAsync();
        }
    }
}
