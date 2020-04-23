using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class PassportTypesModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public List<PassportDurationModel> PassportDurations { get; set; }
    }
}
