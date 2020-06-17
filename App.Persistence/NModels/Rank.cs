using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char? Type { get; set; }
        public int StatusId { get; set; }
        public short? Sorter { get; set; }
        public short? CategoryId { get; set; }
        public int? Rate { get; set; }
    }
}
