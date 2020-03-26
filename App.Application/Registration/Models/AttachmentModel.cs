using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Registration.Models
{
    public class AttachmentModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int AttachmentTypeId { get; set; }
        public string AttachmentType { get; set; }
        public int ProfileId { get; set; }
        public string Path { get; set; }
        public bool IsEncrypted { get; set; }
        public int StatusId { get; set; }
        public string ContentType { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentDateShamsi { get; set; }
        public string EncryptionKey { get; set; }
        public string Description { get; set; }
    }
}
