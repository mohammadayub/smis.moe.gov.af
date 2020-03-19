using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class SystemStatus
    {
        public int Id { get; set; }
        public string StatusType { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Sorter { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
