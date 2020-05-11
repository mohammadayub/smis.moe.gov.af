using App.Application.Management.Models;
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

namespace App.Application.Management.Queries
{
    public class SearchBlackListProfileQuery : IRequest<List<SearchBlackListProfileModel>>
    {
        public int? ID { get; set; }
    }


    public class SearchBlackListProfileQueryHandler : IRequestHandler<SearchBlackListProfileQuery, List<SearchBlackListProfileModel>>
    {
        private AppDbContext Context { get; }
        public SearchBlackListProfileQueryHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SearchBlackListProfileModel>> Handle(SearchBlackListProfileQuery request, CancellationToken cancellationToken)
        {
            var query = Context.BlackListProfiles.AsQueryable();

            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }


            return await query.Select(e => new SearchBlackListProfileModel
            {
                Id = e.Id,
                Code = e.Code,
                
                TitleId = e.TitleId,
                DocumentTypeId = e.DocumentTypeId,
                NID = e.NationalId,
                NIDText = NationalIDReader.ConvertJSONToString(e.NationalId, e.DocumentType.Name),

                Name = e.Name,
                NameEn = e.NameEn,
                FamilyName = e.FamilyName,
                FamilyNameEn = e.FamilyNameEn,
                FatherName = e.FatherName,
                FatherNameEn = e.FatherNameEn,
                GrandFatherName = e.GrandFatherName,
                GrandFatherNameEn = e.GrandFatherNameEn,
                DateOfBirth = e.DateOfBirth.ToString("yyyy-MM-dd"),
                DobShamsi = PersianDate.ToPersianDate(e.DateOfBirth),

                GenderId = e.GenderId,
                GenderText = e.Gender.Name,
                MaritalStatusId = e.MaritalStatusId,
                Height = e.Height,
                HairColorId = e.HairColorId,
                EyeColorId = e.EyeColorId,
                OtherDetail = e.OtherDetail,
                ResidenceCountryId = e.ResidenceCountryId,
                ResidenceCountryText = e.ResidenceCountry.TitleEn,
                BirthCountryId = e.BirthCountryId,
                BirthCountryText = e.BirthCountry.Title,
                BirthProvinceId = e.BirthProvinceId,
                BirthProvinceText = e.BirthProvince.TitleEn,
                OtherNationalityId = e.OtherNationalityId,

                PhotoPath = e.PhotoPath

            }).ToListAsync();
        }
    }
}
