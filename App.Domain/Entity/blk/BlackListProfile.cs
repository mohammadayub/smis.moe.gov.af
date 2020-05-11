using Clean.Domain.Entity.doc;
using App.Domain.Entity.look;
using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.blk
{
    public partial class BlackListProfile
    {
        public BlackListProfile()
        {
            BlackList = new HashSet<BlackList>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? Suffix { get; set; }
        public byte[] Prefix { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MaritalStatusId { get; set; }
        public int GenderId { get; set; }
        public int ResidenceCountryId { get; set; }
        public int Height { get; set; }
        public int BirthCountryId { get; set; }
        public int BirthProvinceId { get; set; }
        public int HairColorId { get; set; }
        public int EyeColorId { get; set; }
        public int DocumentTypeId { get; set; }
        public string NationalId { get; set; }
        public int? OtherNationalityId { get; set; }
        public string OtherDetail { get; set; }
        public string PhotoPath { get; set; }
        public string ProfileId { get; set; }
        public byte[] HashKey { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ReferenceNo { get; set; }

        public virtual Country BirthCountry { get; set; }
        public virtual Province BirthProvince { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Color EyeColor { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Color HairColor { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Country OtherNationality { get; set; }
        public virtual Country ResidenceCountry { get; set; }
        public virtual PersonTitles Title { get; set; }
        public virtual ICollection<BlackList> BlackList { get; set; }
    }
}
