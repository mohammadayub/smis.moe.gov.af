using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class StockIn
    {
        public StockIn()
        {
            Passports = new HashSet<Passports>();
        }

        public int Id { get; set; }
        public int? PassportTypeId { get; set; }
        public int? PassportDurationId { get; set; }
        public int? StartSerial { get; set; }
        public int? EndSerial { get; set; }
        public int? PassportCount { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Passports> Passports { get; set; }
    }
}
