using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class CriminalRecord
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int CrimeTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual CrimeType CrimeType { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
