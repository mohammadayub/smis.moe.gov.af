using System;
using System.Collections.Generic;

namespace App.Persistence.NModels
{
    public partial class Color
    {
        public Color()
        {
            ProfileEyeColor = new HashSet<Profile>();
            ProfileHairColor = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string ColorType { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<Profile> ProfileEyeColor { get; set; }
        public virtual ICollection<Profile> ProfileHairColor { get; set; }
    }
}
