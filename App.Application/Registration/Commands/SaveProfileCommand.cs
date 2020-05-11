using App.Application.Registration.Models;
using App.Application.Registration.Queries;
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
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Registration.Commands
{
    public class SaveProfileCommand : IRequest<List<SearchedProfileModel>>
    {
        public bool AllowDuplicates { get; set; } = false;
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

        public long? BId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class SaveProfileCommandHandler : IRequestHandler<SaveProfileCommand, List<SearchedProfileModel>>
    {
        private AppDbContext Context { get; set; }
        private ICurrentUser CurrentUser { get; set; }
        private IMediator Mediator { get; set; }
        public SaveProfileCommandHandler(AppDbContext db,ICurrentUser currentUser,IMediator mediator)
        {
            Context = db;
            CurrentUser = currentUser;
            Mediator = mediator;
        }
        public async Task<List<SearchedProfileModel>> Handle(SaveProfileCommand request, CancellationToken cancellationToken)
        {
            var UserID = await CurrentUser.GetUserId();
            var result = new List<SearchedProfileModel>();
            if (request.Id.HasValue)
            {
                var apps = await Context.PassportApplications.Where(e => e.ProfileId == request.Id).ToListAsync();
                if (apps.Any())
                {
                    var capp = apps.OrderByDescending(e => e.Id).First();
                    if(capp.CurProcessId != SystemProcess.Registration && capp.CurProcessId != SystemProcess.Close)
                    {
                        throw new BusinessRulesException("این درخواست قابل تغییر نمی باشد!");
                    }
                }

                var prf = Context.Profiles.Where(e => e.Id == request.Id).Single();
                var bio = Context.BioDatas.Where(e => e.Id == request.BId && e.StatusId == 1).Single();

                prf.TitleId = request.TitleId;
                prf.GenderId = request.GenderId;
                prf.HairColorId = request.HairColorId;
                prf.EyeColorId = request.EyeColorId;
                prf.BirthCountryId = request.BirthCountryId;
                prf.BirthProvinceId = request.BirthProvinceId;
                prf.DocumentTypeId = request.DocumentTypeId;
                prf.MaritalStatusId = request.MaritalStatusId;
                prf.Height = request.Height;
                prf.NationalId = request.NID;
                prf.OtherNationalityId = request.OtherNationalityId;
                prf.OtherDetail = request.OtherDetail;
                prf.ResidenceCountryId = request.ResidenceCountryId;

                prf.ModifiedBy = UserID;
                prf.ModifiedOn = DateTime.Now;

                var nbio = new BioData
                {
                    Name = request.Name,
                    FamilyName = request.FamilyName,
                    FatherName = request.FatherName,
                    GrandFatherName = request.GrandFatherName,
                    NameEn = request.NameEn,
                    FamilyNameEn = request.FamilyNameEn,
                    FatherNameEn = request.FatherNameEn,
                    GrandFatherNameEn = request.GrandFatherNameEn,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    StatusId = 1,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserID
                };
                nbio.HashKey = GeneratBioDataHash(nbio);
                byte[] arr;
                if(!ByteArrayEquals( bio.HashKey, nbio.HashKey))
                {
                    prf.BioData.Add(nbio);
                    bio.StatusId = 0;

                    arr = GenerateProfileHash(prf, nbio);
                }
                else
                {
                    arr = GenerateProfileHash(prf, bio);
                }
                if(Context.BlackListProfiles.Where(e => e.HashKey == arr && e.StatusId == BlackListStatus.Active).Any())
                {
                    throw new BusinessRulesException("این فرد شامل لیست سیاه می باشد!");
                }
                var hashes = await Context.ProfileHashes.Where(e => e.HashKey == arr).ToListAsync();

                if (!hashes.Any(e => e.ProfileId == prf.Id))
                {
                    ProfileHash ph = new ProfileHash
                    {
                        HashKey = arr
                    };
                    prf.ProfileHash.Add(ph);
                }

                await Context.SaveChangesAsync();
                result = await Mediator.Send(new SearchProfileQuery { ID = prf.Id });
            }
            else
            {
                using(var trans = Context.Database.BeginTransaction())
                {
                    try
                    {
                        var profile = new Profile()
                        {
                            TitleId = request.TitleId,
                            GenderId = request.GenderId,
                            HairColorId = request.HairColorId,
                            EyeColorId = request.EyeColorId,
                            BirthCountryId = request.BirthCountryId,
                            BirthProvinceId = request.BirthProvinceId,
                            DocumentTypeId = request.DocumentTypeId,
                            MaritalStatusId = request.MaritalStatusId,
                            Height = request.HairColorId,
                            NationalId = request.NID,
                            OtherNationalityId = request.OtherNationalityId,
                            OtherDetail = request.OtherDetail,
                            ResidenceCountryId = request.ResidenceCountryId,
                            CreatedBy = UserID,
                            CreatedOn = DateTime.Now,
                            StatusId = ProfileStatus.Active
                        };
                        var bio = new BioData
                        {
                            Name = request.Name,
                            FamilyName = request.FamilyName,
                            FatherName = request.FatherName,
                            GrandFatherName = request.GrandFatherName,
                            NameEn = request.NameEn,
                            FamilyNameEn = request.FamilyNameEn,
                            FatherNameEn = request.FatherNameEn,
                            GrandFatherNameEn = request.GrandFatherNameEn,
                            DateOfBirth = request.DateOfBirth,
                            Email = request.Email,
                            PhoneNumber = request.PhoneNumber,
                            StatusId = 1,
                            CreatedOn = DateTime.Now,
                            CreatedBy = UserID
                        };
                        bio.HashKey = GeneratBioDataHash(bio);

                        var profileHash = new ProfileHash
                        {
                            HashKey = GenerateProfileHash(profile, bio)
                        };
                        if (request.AllowDuplicates)
                        {
                            var cd = await GenerateCodeAsync(bio.DateOfBirth,profile.BirthProvinceId);
                            profile.Code = cd.Item1;
                            profile.Suffix = cd.Item2;
                            profile.Prefix = cd.Item3.GetBytes();
                            profile.BioData.Add(bio);
                            profile.ProfileHash.Add(profileHash);
                            Context.Profiles.Add(profile);
                            await Context.SaveChangesAsync();
                            
                        }
                        else
                        {
                            if (Context.BlackListProfiles.Where(e => e.HashKey == profileHash.HashKey && e.StatusId == BlackListStatus.Active).Any())
                            {
                                throw new BusinessRulesException("این فرد شامل لیست سیاه می باشد!");
                            }
                            var ex = Context.ProfileHashes.Where(e => e.HashKey == profileHash.HashKey).Select(e => new { e.ProfileId,e.Profile.Code }).FirstOrDefault();
                            
                            if(ex  != null)
                            {
                                throw new BusinessRulesException(String.Format( "این شخص قبلا ثبت شده است با کود {0}",ex.Code));
                            }
                            else 
                            {
                                var cd = await GenerateCodeAsync(bio.DateOfBirth, profile.BirthProvinceId);
                                profile.Code = cd.Item1;
                                profile.Suffix = cd.Item2;
                                profile.Prefix = cd.Item3.GetBytes();
                                profile.BioData.Add(bio);
                                profile.ProfileHash.Add(profileHash);
                                Context.Profiles.Add(profile);
                                await Context.SaveChangesAsync();
                            }
                        }
                        await trans.CommitAsync();
                        result = await Mediator.Send(new SearchProfileQuery { ID = profile.Id });
                    }
                    catch(Exception ex)
                    {
                        await trans.RollbackAsync();
                        throw ex;
                    }
                }

            }
            return result;
        }

        private byte[] GenerateProfileHash(Profile profile, BioData bio)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CleanString(bio.Name));
            sb.Append(CleanString(bio.FatherName));
            sb.Append(CleanString(bio.GrandFatherName));
            sb.Append(bio.DateOfBirth.Year);
            sb.Append(bio.DateOfBirth.Month);
            sb.Append(profile.BirthCountryId);
            sb.Append(profile.BirthProvinceId);
            using MD5 md = MD5.Create();
            return md.ComputeHash(sb.ToString().GetBytes());
        }

        private byte[] GeneratBioDataHash(BioData bio)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CleanString(bio.Name));
            sb.Append(CleanString(bio.FatherName));
            sb.Append(CleanString(bio.GrandFatherName));
            sb.Append(CleanString(bio.FamilyName));
            sb.Append(CleanString(bio.NameEn));
            sb.Append(CleanString(bio.FatherNameEn));
            sb.Append(CleanString(bio.GrandFatherNameEn));
            sb.Append(CleanString(bio.FamilyNameEn));
            sb.Append(bio.DateOfBirth.ToString("yyyy-MM-dd"));
            sb.Append(String.IsNullOrEmpty(bio.Email) ? "NE" : bio.Email);
            sb.Append(String.IsNullOrEmpty(bio.PhoneNumber) ? "NN" : bio.Email);
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

        public async Task<Tuple<string,int?,string>> GenerateCodeAsync(DateTime birthDate,int birthProvince)
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
            var last = await Context.Profiles.Where(p => p.Prefix == PrefixBuilder.ToString().GetBytes()).OrderByDescending(e => e.Suffix).FirstOrDefaultAsync();
            int? CurrentSuffix = last == null ? 0 : last.Suffix;
            if (CurrentSuffix is null) CurrentSuffix = 0;
            Suffix = CurrentSuffix + 1;

            // Build Code
            CodeBuilder.Append(PrefixBuilder.ToString());
            CodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
            return new Tuple<string, int?, string>(CodeBuilder.ToString(),Suffix,PrefixBuilder.ToString()) ;
        }

        public bool ByteArrayEquals(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }

    }

}
