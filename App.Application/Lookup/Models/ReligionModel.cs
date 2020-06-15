using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
   public class ReligionModel
    {
        public ReligionModel()
        {
            Profiles = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
