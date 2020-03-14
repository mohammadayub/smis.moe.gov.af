using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class PassportDuration
    {
        public PassportDuration()
        {
            PassportApplication = new HashSet<PassportApplication>();
            PaymentConfig = new HashSet<PaymentConfig>();
        }

        public int Id { get; set; }
        public int PassportTypeId { get; set; }
        public int Months { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PassportType PassportType { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
        public virtual ICollection<PaymentConfig> PaymentConfig { get; set; }
    }
}
