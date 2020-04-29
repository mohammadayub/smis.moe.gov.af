using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class DisabledReason
    {
        public DisabledReason()
        {
            DisabledPassport = new HashSet<DisabledPassport>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<DisabledPassport> DisabledPassport { get; set; }
    }
}
