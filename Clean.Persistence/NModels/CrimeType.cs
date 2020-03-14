using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class CrimeType
    {
        public CrimeType()
        {
            CriminalRecord = new HashSet<CriminalRecord>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<CriminalRecord> CriminalRecord { get; set; }
    }
}
