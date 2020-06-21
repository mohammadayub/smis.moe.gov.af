﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Prf.Models
{
    public class SearchProfileModel
    {
        public string FatherName { get; set; }
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? EthnicityId { get; set; }
        public int? ReligionId { get; set; }
        public string Comments { get; set; }
        public string Remark { get; set; }
        public int? BloodGroupId { get; set; }
        public string GenderText { get; set; }
        public string BloodGroupText { get; set; }
        public string EthnicityText { get; set; }
        public string BirthLocationText { get; set; }
        public string ReligionText { get; set; }
        public string MaritalStatusText { get; set; }
        public int Province { get; set; }
        public string ProvinceText { get; set; }
        public int District { get; set; }
        public string DistrictText { get; set; }
        public int? DocumentTypeId { get; set; }
        public string PhotoPath { get; set; }
        public string NID { get; set; }

        public string DocumentTypeText { get; set; }

        public string NIDText { get; set; }

        public string DoBText { get; set; }
        public string Salary { get; set; }
        public string JobSalary { get; set; }
        public string TazkiraNumber { get; set; }
        public string Mobile { get; set; }

        public string CreatedOn { get; set; }
    }
}
