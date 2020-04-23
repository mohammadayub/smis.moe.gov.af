using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Printing.Models
{
    public class PassportPrintProcessModel
    {
        public bool Marked { get; set; }
        public string MarkError { get; set; }
        public bool Processed { get; set; }
        public string ProcessError { get; set; }
    }
}
