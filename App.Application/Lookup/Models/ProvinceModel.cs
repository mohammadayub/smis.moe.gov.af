using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class ProvinceModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Country { get; set; }
        public int CountryID { get; set; }
        public string Code { get; set; }
    }
}
