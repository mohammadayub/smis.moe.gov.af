using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class DisabledPassport
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int DisabledReasonId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Comment { get; set; }

        public virtual PassportApplication Application { get; set; }
        public virtual DisabledReason DisabledReason { get; set; }
    }
}
