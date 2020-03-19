using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Discounts
    {
        public Discounts()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public int DiscountTypeId { get; set; }
        public string Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? OfficeId { get; set; }

        public virtual DiscountType DiscountType { get; set; }
        public virtual Office Office { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
