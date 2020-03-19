using App.Domain.Entity.prt;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.qc
{
    public partial class QualityControl
    {
        public long Id { get; set; }
        public long PassportPrintId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual PassportPrint PassportPrint { get; set; }
    }
}
