using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class OperationType
    {
        public OperationType()
        {
            Audit = new HashSet<Audit>();
        }

        public int Id { get; set; }
        public string OperationTypeName { get; set; }

        public virtual ICollection<Audit> Audit { get; set; }
    }
}
