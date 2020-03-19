using System;
using System.Collections.Generic;

namespace App.Domain.Entity.doc
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Documents = new HashSet<Documents>();
            ScreenDocument = new HashSet<ScreenDocument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Documents> Documents { get; set; }
        public virtual ICollection<ScreenDocument> ScreenDocument { get; set; }
    }
}
