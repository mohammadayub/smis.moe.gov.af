using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class District
    {
        public int Id { get; set; }
        public int? ProvinceId { get; set; }
        public string Name { get; set; }
    }
}
