using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf.view
{
    public class ProfileProcess
    {
        public decimal ID { get; set; }
        public int ServiceTypeID { get; set; }
        public decimal? ApplicationID { get; set; }
        public short? ProcessID { get; set; }
    }
}
