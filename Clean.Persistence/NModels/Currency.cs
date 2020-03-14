﻿using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class Currency
    {
        public Currency()
        {
            Office = new HashSet<Office>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Office> Office { get; set; }
    }
}