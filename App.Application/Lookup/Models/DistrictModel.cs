using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class DistrictModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int ProvinceID { get; set; }
        public string Province { get; set; }
    }
}
