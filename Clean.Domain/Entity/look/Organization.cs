using System.Collections.Generic;

namespace Clean.Domain.Entity.look
{
    public class Organization
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short OrganizationTypeId { get; set; }


        //public virtual ICollection<Profile> Profiles { get; set; }

    }
}
