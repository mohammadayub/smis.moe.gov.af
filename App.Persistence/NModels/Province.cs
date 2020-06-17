using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Province
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public string TitleEn { get; set; }
    }
}
