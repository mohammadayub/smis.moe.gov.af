﻿using App.Domain.Entity.pas;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prc
{
    public partial class ResearchQueue
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool Processed { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public virtual PassportApplication Application { get; set; }
    }
}