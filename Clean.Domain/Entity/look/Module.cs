using Clean.Domain.Entity.prc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Domain.Entity.look
{
    public class Module
    {
        public Module()
        {
            ProcessTracking = new HashSet<ProcessTracking>();
            Screen = new HashSet<Screen>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Sorter { get; set; }

        public virtual ICollection<ProcessTracking> ProcessTracking { get; set; }
        public virtual ICollection<Screen> Screen { get; set; }
    }
}
