using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Registration.Models
{
    public class CriminalRecordModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int CrimeTypeId { get; set; }
        public string Date { get; set; }
        public string DateShamsi { get; set; }
        public string Description { get; set; }

        public string CrimeType { get; set; }
    }
}
