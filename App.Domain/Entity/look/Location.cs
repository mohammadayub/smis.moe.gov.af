using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public class Location
    {
        public Location()
        {
            //ApplicationEventDistricts = new HashSet<Application>();
            //ApplicationEventProvinces = new HashSet<Application>();

            //ProfileBirthLocations = new HashSet<Profile>();
            //ProfileDistrictNavigations = new HashSet<Profile>();
            //ProfileProvinceNavigations = new HashSet<Profile>();
            //SchoolDistricts = new HashSet<SchoolInformation>();

            //SchoolProvinces = new HashSet<SchoolInformation>();




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

        //public virtual ICollection<Application> ApplicationEventDistricts { get; set; }
        //public virtual ICollection<Application> ApplicationEventProvinces { get; set; }
        //public virtual ICollection<SchoolInformation> SchoolDistricts { get; set; }

        //public virtual ICollection<SchoolInformation> SchoolProvinces { get; set; }
        public virtual ICollection<Profile> ProfileBirthLocations { get; set; }
        public virtual ICollection<Profile> ProfileDistrictNavigations { get; set; }
        public virtual ICollection<Profile> ProfileProvinceNavigations { get; set; }



    }
}
