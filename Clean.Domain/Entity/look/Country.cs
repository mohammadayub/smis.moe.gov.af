using System;
using System.Collections.Generic;

namespace Clean.Domain.Entity.look
{
    public partial class Country
    {
        public Country()
        {
            Province = new HashSet<Province>();
        }

        public string Title { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Province> Province { get; set; }
    }
}
