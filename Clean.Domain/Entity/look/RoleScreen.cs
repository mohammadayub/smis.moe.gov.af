using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Domain.Entity.look
{
    public class RoleScreen
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ScreenId { get; set; }
        public bool IsActive { get; set; }

        public virtual Screen Screen { get; set; }
    }
}
