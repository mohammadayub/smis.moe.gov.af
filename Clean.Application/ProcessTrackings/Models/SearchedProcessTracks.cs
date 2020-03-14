using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.ProcessTrackings.Models
{
    public class SearchedProcessTracks
    {
        public long Id { get; set; }
        public long RecordId { get; set; }
        public int ProcessId { get; set; }
        public int ReferedProcessId { get; set; }
        public int StatusId { get; set; }
        public string StatusText { get; set; }
        public string Remarks { get; set; }
        public int? ModuleId { get; set; }
        public string ProcessText { get; set; }
        public string ModuleText { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DateText { get; set; }
        public string TimeText { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? ToUserId { get; set; }
        public string ToUserName { get; set; }
    }
}
