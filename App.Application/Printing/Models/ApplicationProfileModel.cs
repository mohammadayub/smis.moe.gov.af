using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class ApplicationProfileModel
    {
        public int Id { get; set; }
        public int ProfileID { get; set; }
        public string Code { get; set; }
        public string ProfileCode { get; set; }
        public int MaritalStatusId { get; set; }
        public int GenderId { get; set; }
        public string GenderText { get; set; }
        public string GenderEn { get; set; }
        public int ResidenceCountryId { get; set; }
        public string ResidenceCountryText { get; set; }
        public int Height { get; set; }
        public int BirthCountryId { get; set; }
        public string BirthCountryText { get; set; }
        public string BirthCountryTextEn { get; set; }
        public int BirthProvinceId { get; set; }
        public string BirthProvinceText { get; set; }
        public int HairColorId { get; set; }
        public int EyeColorId { get; set; }
        public string OtherDetail { get; set; }
        public int DocumentTypeId { get; set; }
        public string NID { get; set; }
        public string NIDText { get; set; }
        public string NIDSerial { get; set; }
        public int TitleId { get; set; }
        public int? OtherNationalityId { get; set; }

        public long BId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
        public DateTime DateOfBirthFull { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthPassport { get; set; }
        public string DobShamsi { get; set; }
        public string DobShamsiPassport { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
