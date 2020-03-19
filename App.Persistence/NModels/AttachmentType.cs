using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class AttachmentType
    {
        public AttachmentType()
        {
            Attachments = new HashSet<Attachments>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Attachments> Attachments { get; set; }
    }
}
