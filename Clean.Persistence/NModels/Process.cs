﻿using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Process
    {
        public Process()
        {
            ProcessConnectionConnectedToNavigation = new HashSet<ProcessConnection>();
            ProcessConnectionProcess = new HashSet<ProcessConnection>();
            ProcessTrackingProcess = new HashSet<ProcessTracking>();
            ProcessTrackingReferedProcess = new HashSet<ProcessTracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ScreenId { get; set; }
        public string Sorter { get; set; }

        public virtual Screen Screen { get; set; }
        public virtual ICollection<ProcessConnection> ProcessConnectionConnectedToNavigation { get; set; }
        public virtual ICollection<ProcessConnection> ProcessConnectionProcess { get; set; }
        public virtual ICollection<ProcessTracking> ProcessTrackingProcess { get; set; }
        public virtual ICollection<ProcessTracking> ProcessTrackingReferedProcess { get; set; }
    }
}
