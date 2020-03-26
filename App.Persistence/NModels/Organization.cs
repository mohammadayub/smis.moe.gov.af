using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Organization
    {
        public Organization()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            Job = new HashSet<Job>();
            Occupation = new HashSet<Occupation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short OrganizationTypeId { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<Occupation> Occupation { get; set; }
    }
}
