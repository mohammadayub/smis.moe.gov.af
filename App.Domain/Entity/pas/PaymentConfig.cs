using System;
using System.Collections.Generic;

namespace App.Domain.Entity.pas
{
    public partial class PaymentConfig
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int PassportTypeId { get; set; }
        public int PassportDurationId { get; set; }
        public int PaymentCategoryId { get; set; }
        public double Amount { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PassportDuration PassportDuration { get; set; }
        public virtual PassportType PassportType { get; set; }
        public virtual PaymentCategory PaymentCategory { get; set; }
    }
}
