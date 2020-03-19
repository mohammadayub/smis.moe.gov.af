using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Province
    {
        public Province()
        {
            Address = new HashSet<Address>();
            District = new HashSet<District>();
            Office = new HashSet<Office>();
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public string TitleEn { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<District> District { get; set; }
        public virtual ICollection<Office> Office { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
