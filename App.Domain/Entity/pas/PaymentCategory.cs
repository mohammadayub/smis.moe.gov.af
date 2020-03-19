using System;
using System.Collections.Generic;

namespace App.Domain.Entity.pas
{
    public partial class PaymentCategory
    {
        public PaymentCategory()
        {
            PassportApplication = new HashSet<PassportApplication>();
            PaymentConfig = new HashSet<PaymentConfig>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double AmountChange { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
        public virtual ICollection<PaymentConfig> PaymentConfig { get; set; }
    }
}
