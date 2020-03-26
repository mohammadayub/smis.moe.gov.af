using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class PassportDiscountsModel
    {
        public int? Id { get; set; }
        public int DiscountTypeId { get; set; }
        public string DiscountType { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public string ActiveFrom { get; set; }
        public string ActiveFromShamsi { get; set; }
        public string ActiveTo { get; set; }
        public string ActiveToShamsi { get; set; }
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public string Name { get; set; }
    }
}
