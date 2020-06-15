using App.Domain.Entity.prf;
using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
   public  class DocumentType
    {
        public DocumentType()
        {

            Profiles = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ScreenId { get; set; }
        public string Category { get; set; }
        public virtual Screen Screen { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
