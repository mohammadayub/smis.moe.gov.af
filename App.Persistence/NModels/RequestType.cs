using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class RequestType
    {
        public RequestType()
        {
            PassportApplication = new HashSet<PassportApplication>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public double? OverPayment { get; set; }

        public virtual ICollection<PassportApplication> PassportApplication { get; set; }
    }
}
