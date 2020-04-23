using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class ApplicationJobModel
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
        public string OccupationEn { get; set; }
    }
}
