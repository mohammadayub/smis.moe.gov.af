using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Registration.Models
{
    public class SearchAddressModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int DistrictId { get; set; }
        public string Village { get; set; }
        public string Detail { get; set; }
        public int ProfileId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressType { get; set; }
    }
}
