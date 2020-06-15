using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    class ApplicationReason
    {
        public ApplicationReason()
        {
            Application = new HashSet<Application>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Application> Application { get; set; }
    }
}
