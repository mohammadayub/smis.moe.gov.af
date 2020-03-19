using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PassportType
    {
        public PassportType()
        {
            PassportApplication = new HashSet<PassportApplication>();
            PassportDuration = new HashSet<PassportDuration>();
            PaymentConfig = new HashSet<PaymentConfig>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int SerialLength { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
        public virtual ICollection<PassportDuration> PassportDuration { get; set; }
        public virtual ICollection<PaymentConfig> PaymentConfig { get; set; }
    }
}
