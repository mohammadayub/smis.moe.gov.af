using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class PassportDurationModel
    {
        public int ID { get; set; }
        public int PassportTypeID { get; set; }
        public int Months { get; set; }
        public string PassportType { get; set; }
    }
}
