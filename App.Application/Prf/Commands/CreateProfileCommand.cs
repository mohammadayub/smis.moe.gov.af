using App.Application.Prf.Models;
using App.Application.Prf.Queries;
using App.Persistence.Context;
using Clean.Common.Extensions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Prf.Commands
{
    public class CreateProfileCommand : IRequest<List<SearchProfileModel>>
    {
        public string FatherName { get; set; }
        public decimal? Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }

        public string ReferenceNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string GrandFatherName { get; set; }

        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public short? MaritalStatusId { get; set; }
        public int? EthnicityId { get; set; }
        public short? ReligionID { get; set; }
        public int? StatusID { get; set; }
        public int? BloodGroupID { get; set; }
        public int? DocumentTypeId { get; set; }

        public int? ServiceTypeID { get; set; }
        public string PhotoPath { get; set; }
        public string NID { get; set; }

        public int Province { get; set; }
        public int District { get; set; }
        public string Mobile { get; set; }

        public string M40Detail { get; set; }
    }



    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, List<SearchProfileModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateProfileCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<SearchProfileModel>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<SearchProfileModel> result = new List<SearchProfileModel>();
            var profile = request.Id.HasValue ? context.Profiles.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Profile();
            int CurrentUserId = await currentUser.GetUserId();

            profile.FirstName = request.FirstName.Trim();
            profile.LastName = request.LastName.Trim();
            profile.FatherName = request.FatherName;
            profile.GrandFatherName = request.GrandFatherName;
            profile.FirstNameEng = request.FirstNameEng;
            profile.LastNameEng = request.LastNameEng;
            profile.FatherNameEng = request.FatherNameEng;
            profile.DateOfBirth = request.DateOfBirth.Value;
            profile.BirthLocationId = request.BirthLocationId.Value;
            profile.GenderId = request.GenderId.Value;
            profile.MaritalStatusId = request.MaritalStatusId.Value;
            profile.EthnicityId = request.EthnicityId.Value;
            profile.ReligionId = request.ReligionID.Value;
            profile.BloodGroupId = request.BloodGroupID;
            profile.NationalId = request.NID;
            profile.PhotoPath = request.PhotoPath;
            profile.DocumentTypeId = request.DocumentTypeId.Value;
            profile.Province = request.Province;
            profile.District = request.District;
            profile.Mobile = request.Mobile;
            //profile.M40Detail = request.M40Detail;

            if (!request.Id.HasValue)
            {
                #region BuildHrCode
                StringBuilder PrefixBuilder = new StringBuilder(string.Empty);
                StringBuilder HrCodeBuilder = new StringBuilder(string.Empty);

                // Build Prefix
                PrefixBuilder.Append("S");
                PrefixBuilder.Append(("00" + request.BirthLocationId.ToString()).Right(2));
                PrefixBuilder.Append(("00" + Convert.ToDateTime(request.DateOfBirth).Year.ToString()).Right(2));
                PrefixBuilder.Append(("00" + Convert.ToDateTime(request.DateOfBirth).Month.ToString()).Right(2));

                //Build Suffix
                //Get Current Suffix where its prefix is equal to PrefixBuilder.
                int? Suffix;
                var last = await context.Profiles.Where(p => p.Prefix == PrefixBuilder.ToString()).OrderByDescending(e => e.Suffix).FirstOrDefaultAsync();
                int? CurrentSuffix = last == null ? 0 : last.Suffix;
                if (CurrentSuffix is null) CurrentSuffix = 0;
                Suffix = CurrentSuffix + 1;

                // Build HR Code
                HrCodeBuilder.Append(PrefixBuilder.ToString());
                HrCodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
                #endregion BuildHrCode

                profile.Code = HrCodeBuilder.ToString();
                profile.Prefix = PrefixBuilder.ToString();
                profile.Suffix = Suffix;

                profile.StatusId = 1;
                profile.ModifiedBy = "";
                profile.ModifiedOn = DateTime.Now;
                profile.CreatedBy = CurrentUserId;
                profile.CreatedOn = DateTime.Now;
                profile.OrganizationId = (await currentUser.GetUserOrganizationID()).Value;

                profile.ServiceTypeId = request.ServiceTypeID.Value;

                context.Profiles.Add(profile);
            }
            else
            {
                profile.StatusId = request.StatusID.HasValue ? request.StatusID.Value : 1;
                profile.ModifiedBy += "," + CurrentUserId;
                profile.ModifiedOn = DateTime.Now;
            }


            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchProfileQuery() { ID = profile.Id });

            return result.ToList();

        }
    }
}
