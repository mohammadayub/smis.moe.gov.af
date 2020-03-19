using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Attachments
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int AttachmentTypeId { get; set; }
        public int ProfileId { get; set; }
        public string Path { get; set; }
        public bool IsEncrypted { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        public virtual AttachmentType AttachmentType { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
