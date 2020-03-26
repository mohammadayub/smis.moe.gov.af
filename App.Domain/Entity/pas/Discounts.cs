using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.pas
{
    public partial class Discounts
    {
        public Discounts()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public int DiscountTypeId { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual DiscountType DiscountType { get; set; }
        public virtual Office Office { get; set; }
        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
