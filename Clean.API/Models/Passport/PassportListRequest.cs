using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.API.Models.Passport
{
    public class PassportListRequest
    {
        [Required]
        public int PassportTypeID { get; set; }

        [Required]
        public int PassportDurationID { get; set; }
    }
}
