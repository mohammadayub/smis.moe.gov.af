using App.Application.Prf.Models;
using App.Persistence.Context;
using Clean.Common.Service;
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

namespace App.Application.Prf.Queries
{
    public class SearchProfileQuery : IRequest<IEnumerable<SearchProfileModel>>
    {
        public decimal? ID { get; set; }
        public string Code { get; set; }
        public int ServiceTypeID { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public int? ProcessID { get; set; }
        public bool? InitialProcess { get; set; }

    }

    public class SearchProfileQueryHandler : IRequestHandler<SearchProfileQuery, IEnumerable<SearchProfileModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchProfileQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }
        public async Task<IEnumerable<SearchProfileModel>> Handle(SearchProfileQuery request, CancellationToken cancellationToken)
        {
            var query = context.Profiles.AsQueryable();
            if (request.ProcessID.HasValue)
            {
                if (request.InitialProcess.HasValue && request.InitialProcess.Value)
                {
                    query = (from f in query
                             join q in context.ProfileProcesses on f.Id equals q.ID
                             where q.ApplicationID == null || q.ProcessID == request.ProcessID
                             select f);
                }
                else
                {
                    query = (from f in query
                             join q in context.ProfileProcesses on f.Id equals q.ID
                             where q.ProcessID == request.ProcessID
                             select f);
                }
            }
            if (!(await User.IsSuperAdmin()).Value)
            {
                var og = await User.GetUserOrganizationID();
                query = query.Where(e => e.OrganizationId == og);
            }

            if (!(await User.IsSuperAdmin()).Value)
            {
                var og = await User.GetOfficeID();

                var provincesUsers = IDContext.Users.Where(e => e.OfficeID == og).Select(e => e.Id).ToList();

                query = query.Where(e => provincesUsers.Contains((int)e.CreatedBy));

            }


            //if (request.ID.HasValue)
            //{
            //    query = query.Where(e => e.Id == request.ID.Value);
            //}

            //else
            //{
            //    query = query.Where(e => e.ServiceTypeId == request.ServiceTypeID);
            //}


            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }

            if (!String.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, String.Concat("%", request.FirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }

            return (await query.Select(p =>  new SearchProfileModel
                          {
                              Id = p.Id,
                              FirstName=p.FirstName,
                              LastName = p.LastName,
                              FatherName = p.FatherName,
                              FirstNameEng=p.FirstNameEng,
                              LastNameEng=p.LastNameEng,
                              FatherNameEng=p.FatherNameEng,
                              GenderId = p.GenderId,
                              DateOfBirth = p.DateOfBirth,
                              GrandFatherName = p.GrandFatherName,


                              PhotoPath = p.PhotoPath,
                              Code = p.Code,
                              BirthLocationId = p.BirthLocationId,
                              MaritalStatusId = p.MaritalStatusId,
                              ReligionId = p.ReligionId,
                              EthnicityId = p.EthnicityId,
                              BloodGroupId = p.BloodGroupId,
                              GenderText = p.Gender.Name,
                              //BloodGroupText = resultPB.Name,
                              EthnicityText = p.Ethnicity.Name,
                              //ReligionText = resultPR.Name,
                              //BirthLocationText = resultPL.Dari,
                              //MaritalStatusText = resultPM.Name,

                              Mobile = p.Mobile,

                              DocumentTypeId = p.DocumentTypeId,

                              DocumentTypeText = p.DocumentType.Name,

                              NID = p.NationalId,

                              
                              //TazkiraNumber = NationalIDReader.ConvertJSONToShortString(p.NationalId, resultPDT.Name) ?? "درج نگردیده",
                              Province = p.Province,
                              ProvinceText =p.ProvinceNavigation.Dari ,
                              District = p.District,
                              DistrictText = p.DistrictNavigation.Dari,


                // CreatedOn = PersianLibrary.PersianDate.GetFormatedString(p.CreatedOn),

                // DoBText = Clean.Common.Dates.PersianDate.GetFormatedString(p.DateOfBirth)

                
            })
                
                .Take(10).OrderBy(x => x.Id).ToListAsync()).Select(cur =>
                {
                    cur.NIDText = NationalIDReader.ConvertJSONToString(cur.NID,cur.DocumentTypeText);
                    return cur;
                }).ToList();
        }
    }

}