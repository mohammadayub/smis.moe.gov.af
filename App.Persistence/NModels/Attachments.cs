﻿using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
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
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string ContentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string EncryptionKey { get; set; }
        public string Description { get; set; }

        public virtual AttachmentType AttachmentType { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
