using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool? HasReceipt { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
