using App.Domain.Entity.blk;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class BlackListReason
    {
        public BlackListReason()
        {
            BlackList = new HashSet<BlackList>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<BlackList> BlackList { get; set; }
    }
}
