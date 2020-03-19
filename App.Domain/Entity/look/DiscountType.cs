using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class DiscountType
    {
        public DiscountType()
        {
            Discounts = new HashSet<Discounts>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Discounts> Discounts { get; set; }
    }
}
