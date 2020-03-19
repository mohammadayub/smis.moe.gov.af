﻿using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Biometric
    {
        public Biometric()
        {
            BiometricDetail = new HashSet<BiometricDetail>();
        }

        public int ProfileId { get; set; }
        public int BiometricTypeId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int Id { get; set; }

        public virtual BiometricType BiometricType { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<BiometricDetail> BiometricDetail { get; set; }
    }
}
