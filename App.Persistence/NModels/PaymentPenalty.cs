using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PaymentPenalty
    {
        public PaymentPenalty()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public int OfficeId { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Office Office { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
