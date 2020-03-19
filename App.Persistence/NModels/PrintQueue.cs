using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PrintQueue
    {
        public PrintQueue()
        {
            PassportPrint = new HashSet<PassportPrint>();
        }

        public long Id { get; set; }
        public int ApplicationId { get; set; }
        public int? UserId { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ProcessedOn { get; set; }

        public virtual PassportApplication Application { get; set; }
        public virtual ICollection<PassportPrint> PassportPrint { get; set; }
    }
}
