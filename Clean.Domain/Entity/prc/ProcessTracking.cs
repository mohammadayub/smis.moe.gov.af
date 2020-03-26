using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace Clean.Domain.Entity.prc
{
    public partial class ProcessTracking
    {
        public long Id { get; set; }
        public long RecordId { get; set; }
        public int ProcessId { get; set; }
        public int ReferedProcessId { get; set; }
        public int StatusId { get; set; }
        public string Remarks { get; set; }
        public int ModuleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
        public int? ToUserId { get; set; }

        public virtual Module Module { get; set; }
        public virtual Process Process { get; set; }
    }
}
