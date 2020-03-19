using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Country
    {
        public Country()
        {
            Address = new HashSet<Address>();
            Office = new HashSet<Office>();
            ProfileBirthCountry = new HashSet<Profile>();
            ProfileOtherNationality = new HashSet<Profile>();
            ProfileResidenceCountry = new HashSet<Profile>();
            Province = new HashSet<Province>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Office> Office { get; set; }
        public virtual ICollection<Profile> ProfileBirthCountry { get; set; }
        public virtual ICollection<Profile> ProfileOtherNationality { get; set; }
        public virtual ICollection<Profile> ProfileResidenceCountry { get; set; }
        public virtual ICollection<Province> Province { get; set; }
    }
}
