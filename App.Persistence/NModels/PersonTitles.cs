﻿using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class PersonTitles
    {
        public PersonTitles()
        {
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}