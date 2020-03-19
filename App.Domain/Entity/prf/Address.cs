using App.Domain.Entity.look;
using App.Domain.Entity.pas;
using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Address
    {
        public Address()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public string City { get; set; }
        public int DistrictId { get; set; }
        public string Village { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int ProfileId { get; set; }
        public int StatusId { get; set; }
        public int AddressTypeId { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual Country Country { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Province Province { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
