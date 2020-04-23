using App.Domain.Entity.prt;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.stc
{
    public partial class Passports
    {
        public Passports()
        {
            PassportPrint = new HashSet<PassportPrint>();
        }

        public long Id { get; set; }
        public int StockInId { get; set; }
        public int SerialNumber { get; set; }
        public string PassportNumber { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModfiedBy { get; set; }

        public virtual StockIn StockIn { get; set; }
        public virtual ICollection<PassportPrint> PassportPrint { get; set; }
    }
}
