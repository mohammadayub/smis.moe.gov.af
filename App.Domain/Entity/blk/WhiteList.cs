using App.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.blk
{
    public partial class WhiteList
    {
        public int Id { get; set; }
        public int BlackListId { get; set; }
        public int RequestedById { get; set; }
        public DateTime WhiteListDate { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual BlackList BlackList { get; set; }
        public virtual Organization RequestedBy { get; set; }

    }
}
