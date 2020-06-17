using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class BloodGroup
    {
        public BloodGroup()
        {
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}
