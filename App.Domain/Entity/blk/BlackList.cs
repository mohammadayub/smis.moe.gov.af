using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.blk
{
    public partial class BlackList
    {
        public int Id { get; set; }
        public int BlackListProfileId { get; set; }
        public int BlackListReasonId { get; set; }
        public DateTime BlackListDate { get; set; }
        public int RequestedById { get; set; }
        public string PassportNumber { get; set; }
        public string Comments { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual BlackListProfile BlackListProfile { get; set; }
        public virtual BlackListReason BlackListReason { get; set; }
        public virtual Organization RequestedBy { get; set; }
    }
}
