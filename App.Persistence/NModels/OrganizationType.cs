using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class OrganizationType
    {
        public OrganizationType()
        {
            Organization = new HashSet<Organization>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Organization> Organization { get; set; }
    }
}
