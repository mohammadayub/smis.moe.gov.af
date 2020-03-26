using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Passport.Models
{
    public class PassportTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int SerialLength { get; set; }
    }
}
