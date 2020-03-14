﻿using System;
using System.Collections.Generic;

namespace Clean.Persistence.NModels
{
    public partial class ProfileHash
    {
        public long Id { get; set; }
        public int? ProfileId { get; set; }
        public byte[] HashKey { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
