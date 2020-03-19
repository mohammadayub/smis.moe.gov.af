using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class BioData
    {
        public BioData()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FamilyName { get; set; }
        public string FamilyNameEn { get; set; }
        public string FatherName { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandFatherName { get; set; }
        public string GrandFatherNameEn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ProfileId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusId { get; set; }
        public byte[] HashKey { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
