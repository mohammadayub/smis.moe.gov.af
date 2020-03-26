using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class PaymentPenaltyModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
