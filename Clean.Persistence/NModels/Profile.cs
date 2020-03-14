using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Profile
    {
        public Profile()
        {
            Address = new HashSet<Address>();
            Attachments = new HashSet<Attachments>();
            BioData = new HashSet<BioData>();
            Biometric = new HashSet<Biometric>();
            CriminalRecord = new HashSet<CriminalRecord>();
            Job = new HashSet<Job>();
            PassportApplication = new HashSet<PassportApplication>();
            ProfileHash = new HashSet<ProfileHash>();
        }

        public int Id { get; set; }
        public int MaritalStatusId { get; set; }
        public int GenderId { get; set; }
        public int ResidenceCountryId { get; set; }
        public int Height { get; set; }
        public int BirthCountryId { get; set; }
        public int BirthProvinceId { get; set; }
        public int HairColorId { get; set; }
        public int EyeColorId { get; set; }
        public string OtherDetail { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ReferenceNo { get; set; }
        public string NationalId { get; set; }
        public string DocumentTypeId { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<BioData> BioData { get; set; }
        public virtual ICollection<Biometric> Biometric { get; set; }
        public virtual ICollection<CriminalRecord> CriminalRecord { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
        public virtual ICollection<ProfileHash> ProfileHash { get; set; }
    }
}
