using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class PassportPrintFullInformation
    {
        public long ID { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationCode { get; set; }
        public int ProfileID { get; set; }
        public string ProfileCode { get; set; }
        // Passport Info
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public DateTime ExpireDateFull { get; set; }
        public string IssueDateShamsi { get; set; }
        public string ExpiryDateShamsi { get; set; }
        public string PassportNumber { get; set; }
        public string PassportType { get; set; }
        // Images 
        public string PersonPhoto { get; set; }
        public string PersonSignature { get; set; }

        // Person Inofrmation
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public DateTime DateOfBirthFull { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthShamsi { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public string NIDSerial { get; set; }
        public string BirthCountry { get; set; }
        public string BirthCountryEN { get; set; }

        // Person Job
        public string Occupation { get; set; }
        public string OccupationEn { get; set; }


        // Issuing Office ID
        public string CountryCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNameEn { get; set; }

        // MRZ 
        public string MRZLineOne { get; set; }
        public string MRZLineTwo { get; set; }
    }
}
