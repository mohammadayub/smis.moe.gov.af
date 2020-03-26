using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class PaymentMethodModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasReceipt { get; set; }
    }
}
