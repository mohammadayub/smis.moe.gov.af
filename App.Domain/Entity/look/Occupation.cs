using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class Occupation
    {
        public Occupation()
        {
            Job = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int OrganizationId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
