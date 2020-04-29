using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Management.Models
{
    public class DisabledReasonModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public bool IsActive { get; set; }
    }
}
