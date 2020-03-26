using App.Domain.Entity.look;
using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Job
    {
        public Job()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public int OccupationId { get; set; }
        public string Employer { get; set; }
        public string EmployerAddress { get; set; }
        public string PrevEmployer { get; set; }
        public string PrevEmployerAddress { get; set; }
        public int ProfileId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int OrganizationId { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
