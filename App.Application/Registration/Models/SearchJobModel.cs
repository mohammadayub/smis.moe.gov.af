using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Registration.Models
{
    public class SearchJobModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int OccupationId { get; set; }
        public string Employer { get; set; }
        public string EmployerAddress { get; set; }
        public string PrevEmployer { get; set; }
        public string PrevEmployerAddress { get; set; }
        public int ProfileId { get; set; }

        public string Organization { get; set; }
        public string Occupation { get; set; }
    }
}
