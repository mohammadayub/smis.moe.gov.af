using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class BiometricType
    {
        public BiometricType()
        {
            Biometric = new HashSet<Biometric>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Biometric> Biometric { get; set; }
    }
}
