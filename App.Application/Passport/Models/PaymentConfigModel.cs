using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class PaymentConfigModel
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public int PassportTypeId { get; set; }
        public string PassportType { get; set; }
        public int PassportDurationId { get; set; }
        public string PassportDuration { get; set; }
        public int PaymentCategoryId { get; set; }
        public string PaymentCategory { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
}
