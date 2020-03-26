using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool HasReceipt { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
