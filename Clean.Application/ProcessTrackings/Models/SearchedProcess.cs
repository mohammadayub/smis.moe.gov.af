using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.ProcessTrackings.Models
{
    public class SearchedProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public int? ScreenId { get; set; }
        public string Sorter { get; set; }
    }
}
