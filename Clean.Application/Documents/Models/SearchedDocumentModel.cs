using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Documents.Models
{
    public class SearchedDocumentModel
    {
        public long? Id { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Module { get; set; }
        public string Item { get; set; }
        public decimal RecordId { get; set; }
        public string Root { get; set; }
        public string Path { get; set; }

        public string EncryptionKey { get; set; }
        public string ReferenceNo { get; set; }
        public int? StatusId { get; set; }
        public int? ScreenId { get; set; }
        public string Description { get; set; }
        public int? DocumentTypeId { get; set; }
        public DateTime LastDownloadDate { get; set; }
        public String DocumentTypeText { get; set; }
        public String DownloadDateText { get; set; }
        public String UploadDateText { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentSource { get; set; }
        public string DocumentDateShamsi { get; set; }
        public DateTime? DocumentDate { get; set; }
    }
}
