using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Organization
    {
        public Organization()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short OrganizationTypeId { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
