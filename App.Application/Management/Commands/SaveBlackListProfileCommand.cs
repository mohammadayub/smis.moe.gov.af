using App.Application.Management.Models;
using App.Application.Management.Queries;
using App.Domain.Entity.blk;
using App.Domain.Entity.pas;
using App.Domain.Entity.prf;
using App.Persistence.Context;
using Clean.Common.Enums;
using Clean.Common.Exceptions;
using Clean.Common.Extensions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Management.Commands
{
    public class SaveBlackListProfileCommand : IRequest<List<SearchBlackListProfileModel>>
    {
        public int? Id { get; set; }
        public int MaritalStatusId { get; set; }
        public int GenderId { get; set; }
        public int ResidenceCountryId { get; set; }
        public int Height { get; set; }
        public int BirthCountryId { get; set; }
        public int BirthProvinceId { get; set; }
        public int HairColorId { get; set; }
        public int EyeColorId { get; set; }
        public string OtherDetail { get; set; }
        public string NID { get; set; }
        public int DocumentTypeId { get; set; }
        public int TitleId { get; set; }
        public int? OtherNationalityId { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string PhotoPath { get; set; }

    }

    public class SaveBlackListProfileCommandHandler : IRequestHandler<SaveBlackListProfileCommand, List<SearchBlackListProfileModel>>
    {
        private AppDbContext Context { get; set; }
        private IMediator Mediator { get; set; }
        private ICurrentUser CurrentUser { get; set; }

        public SaveBlackListProfileCommandHandler(AppDbContext context,IMediator mediator,ICurrentUser currentUser)
        {
            Context = context;
            Mediator = mediator;
            CurrentUser = currentUser;
        }
        public async Task<List<SearchBlackListProfileModel>> Handle(SaveBlackListProfileCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            if (request.Id.HasValue)
            {
                var cur = Context.BlackListProfiles.Where(e => e.Id == request.Id).Single();
                cur.TitleId = request.TitleId;
                cur.Name = request.Name;
                cur.NameEn = request.NameEn;
                cur.FamilyName = request.FamilyName;
                cur.FamilyNameEn = request.FamilyNameEn;
                cur.FatherName = request.FatherName;
                cur.FatherNameEn = request.FatherNameEn;
                cur.GrandFatherName = request.GrandFatherName;
                cur.GrandFatherNameEn = request.GrandFatherNameEn;
                cur.DateOfBirth = request.DateOfBirth;
                cur.MaritalStatusId = request.MaritalStatusId;
                cur.GenderId = request.GenderId;
                cur.ResidenceCountryId = request.ResidenceCountryId;
                cur.Height = request.Height;
                cur.BirthCountryId = request.BirthCountryId;
                cur.BirthProvinceId = request.BirthProvinceId;
                cur.HairColorId = request.HairColorId;
                cur.EyeColorId = request.EyeColorId;
                cur.DocumentTypeId = request.DocumentTypeId;
                cur.NationalId = request.NID;
                cur.OtherNationalityId = request.OtherNationalityId;
                cur.OtherDetail = request.OtherDetail;
                cur.PhotoPath = request.PhotoPath;
                cur.HashKey = GenerateProfileHash(cur);
                cur.ProfileId = GetMathcingProfile(cur);
                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchBlackListProfileQuery { ID = cur.Id });
            }
            else
            {
                var cur = new BlackListProfile
                {
                    TitleId = request.TitleId,
                    Name = request.Name,
                    NameEn = request.NameEn,
                    FamilyName = request.FamilyName,
                    FamilyNameEn = request.FamilyNameEn,
                    FatherName = request.FatherName,
                    FatherNameEn = request.FatherNameEn,
                    GrandFatherName = request.GrandFatherName,
                    GrandFatherNameEn = request.GrandFatherNameEn,
                    DateOfBirth = request.DateOfBirth,
                    MaritalStatusId = request.MaritalStatusId,
                    GenderId = request.GenderId,
                    ResidenceCountryId = request.ResidenceCountryId,
                    Height = request.Height,
                    BirthCountryId = request.BirthCountryId,
                    BirthProvinceId = request.BirthProvinceId,
                    HairColorId = request.HairColorId,
                    EyeColorId = request.EyeColorId,
                    DocumentTypeId = request.DocumentTypeId,
                    NationalId = request.NID,
                    OtherNationalityId = request.OtherNationalityId,
                    OtherDetail = request.OtherDetail,
                    PhotoPath = request.PhotoPath,
                };
                
                cur.HashKey = GenerateProfileHash(cur);

                var prfs = Context.BlackListProfiles.Where(e => e.HashKey == cur.HashKey).ToList();
                if (prfs.Any())
                {
                    throw new BusinessRulesException(String.Format( "این شخص قبلا موجود است : کود پروفایل = {0}",prfs.First().Code));
                }

                var code = await GenerateCodeAsync(cur.DateOfBirth, cur.BirthProvinceId);
                cur.Code = code.Item1;
                cur.Suffix = code.Item2;
                cur.Prefix = code.Item3.GetBytes();

                Context.BlackListProfiles.Add(cur);

                await Context.SaveChangesAsync();
                return await Mediator.Send(new SearchBlackListProfileQuery { ID = cur.Id });
            }
        }

        public async Task<Tuple<string, int?, string>> GenerateCodeAsync(DateTime birthDate, int birthProvince)
        {
            var officeID = await CurrentUser.GetOfficeID();
            var office = Context.Offices.Where(e => e.Id == officeID).Single();
            StringBuilder PrefixBuilder = new StringBuilder(string.Empty);
            StringBuilder CodeBuilder = new StringBuilder(string.Empty);

            // Build Prefix
            PrefixBuilder.Append(office.Code);
            PrefixBuilder.Append(("000" + birthProvince.ToString()).Right(3));
            PrefixBuilder.Append(("00" + birthDate.Year.ToString()).Right(2));
            PrefixBuilder.Append(("00" + birthDate.Month.ToString()).Right(2));

            //Build Suffix
            //Get Current Suffix where its prefix is equal to PrefixBuilder.
            int? Suffix;
            var last = await Context.BlackListProfiles.Where(p => p.Prefix == PrefixBuilder.ToString().GetBytes()).OrderByDescending(e => e.Suffix).FirstOrDefaultAsync();
            int? CurrentSuffix = last == null ? 0 : last.Suffix;
            if (CurrentSuffix is null) CurrentSuffix = 0;
            Suffix = CurrentSuffix + 1;

            // Build Code
            CodeBuilder.Append(PrefixBuilder.ToString());
            CodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
            return new Tuple<string, int?, string>(CodeBuilder.ToString(), Suffix, PrefixBuilder.ToString());
        }


        private string GetMathcingProfile(BlackListProfile cur)
        {
            if(Context.BlackLists.Where(e => e.BlackListProfileId == cur.Id).Any())
            {
                var list = new List<Profile>();
                if (!String.IsNullOrEmpty(cur.ProfileId))
                {
                    var prfIds = cur.ProfileId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => Convert.ToInt32(e));
                    list = Context.Profiles.Where(e =>  prfIds.Contains(e.Id)).ToList();
                }
                var matches = Context.ProfileHashes.Where(e => e.HashKey == cur.HashKey).Select(e => e.Profile).Distinct().ToList();
                matches.ForEach(e => e.StatusId = ProfileStatus.BlackList);
                list.RemoveAll(e => matches.Select(f => f.Id).Contains(e.Id));
                list.ForEach(e => e.StatusId = ProfileStatus.Active);
                if (matches.Any())
                {
                    return matches.Select(e => e.Id.ToString()).Aggregate((s1, s2) => String.Concat(s1, ',', s2));
                }
            }
            return null;
        }

        private byte[] GenerateProfileHash(BlackListProfile profile)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CleanString(profile.Name));
            sb.Append(CleanString(profile.FatherName));
            sb.Append(CleanString(profile.GrandFatherName));
            sb.Append(profile.DateOfBirth.Year);
            sb.Append(profile.DateOfBirth.Month);
            sb.Append(profile.BirthCountryId);
            sb.Append(profile.BirthProvinceId);
            using MD5 md = MD5.Create();
            return md.ComputeHash(sb.ToString().GetBytes());
        }

        public string CleanString(string text)
        {
            return text.Replace(" ", "")
                .Replace("‌", "")
                .Replace("ګ", "گ")
                .Replace("ي", "ی");
        }

        public bool ByteArrayEquals(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }
    }
}
