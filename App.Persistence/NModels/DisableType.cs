using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class DisableType
    {
        public int Id { get; set; }
        public int? DFrom { get; set; }
        public int? DTo { get; set; }
        public string Name { get; set; }
    }
}
