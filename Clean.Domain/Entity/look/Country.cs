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

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Province> Province { get; set; }
    }
}
