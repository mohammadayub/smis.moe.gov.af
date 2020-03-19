using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PassportPrint
    {
        public PassportPrint()
        {
            QualityControl = new HashSet<QualityControl>();
        }

        public long Id { get; set; }
        public long PrintQueueId { get; set; }
        public long PassportId { get; set; }
        public DateTime PrintedDate { get; set; }
        public DateTime ValidTo { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

        public virtual Passports Passport { get; set; }
        public virtual PrintQueue PrintQueue { get; set; }
        public virtual ICollection<QualityControl> QualityControl { get; set; }
    }
}
