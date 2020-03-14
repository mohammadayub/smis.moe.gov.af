using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
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

        public virtual ICollection<Job> Job { get; set; }
    }
}
