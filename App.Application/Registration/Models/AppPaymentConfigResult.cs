using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Registration.Models
{
    public class AppPaymentConfigResult
    {
        public bool Exists { get; set; }
        public double Amount { get; set; }
        public bool HasPenalty { get; set; }
        public double Penalty { get; set; }
        public bool HasDiscount { get; set; }
        public double Discount { get; set; }
        public string DiscountType { get; set; }
    }
}
