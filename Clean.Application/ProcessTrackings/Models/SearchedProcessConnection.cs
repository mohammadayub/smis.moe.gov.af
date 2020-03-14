using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.ProcessTrackings.Models
{
    public class SearchedProcessConnection
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int ConnectionId { get; set; }
        public int ModuleId { get; set; }
        public string ConnectionText { get; set; }
        public string ProcessText { get; set; }
        public int? ScreenId { get; set; }
        public string Sorter { get; set; }
    }
}
