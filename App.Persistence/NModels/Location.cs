using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Location
    {
        public Location()
        {
            Office = new HashSet<Office>();
            ProfileBirthLocation = new HashSet<Profile>();
            ProfileCdistrictNavigation = new HashSet<Profile>();
            ProfileCprovinceNavigation = new HashSet<Profile>();
            ProfileDistrictNavigation = new HashSet<Profile>();
            ProfileProvinceNavigation = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public string PathDari { get; set; }
        public int? ParentId { get; set; }
        public int? TypeId { get; set; }

        public virtual ICollection<Office> Office { get; set; }
        public virtual ICollection<Profile> ProfileBirthLocation { get; set; }
        public virtual ICollection<Profile> ProfileCdistrictNavigation { get; set; }
        public virtual ICollection<Profile> ProfileCprovinceNavigation { get; set; }
        public virtual ICollection<Profile> ProfileDistrictNavigation { get; set; }
        public virtual ICollection<Profile> ProfileProvinceNavigation { get; set; }
    }
}
