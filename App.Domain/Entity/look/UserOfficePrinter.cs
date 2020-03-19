using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class UserOfficePrinter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public virtual Office Office { get; set; }
    }
}
