using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class SearchOfficeModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }

        //public int CurrencyId { get; set; }
        public int OrganizationId { get; set; }
        public int OfficeTypeId { get; set; }
    }
}
