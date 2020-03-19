using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class BiometricDetail
    {
        public int Id { get; set; }
        public string BiometricCode { get; set; }
        public int BiometricId { get; set; }
        public int FingerIndex { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Biometric Biometric { get; set; }
    }
}
