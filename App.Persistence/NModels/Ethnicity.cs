using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Ethnicity
    {
        public Ethnicity()
        {
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? CountryId { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}
