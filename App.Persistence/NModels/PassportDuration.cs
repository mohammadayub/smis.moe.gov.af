﻿using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PassportDuration
    {
        public PassportDuration()
        {
            PassportApplication = new HashSet<PassportApplication>();
            PaymentConfig = new HashSet<PaymentConfig>();
            StockIn = new HashSet<StockIn>();
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
        public virtual ICollection<StockIn> StockIn { get; set; }
    }
}
