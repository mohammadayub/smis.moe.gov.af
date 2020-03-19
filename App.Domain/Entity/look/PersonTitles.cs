using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class PersonTitles
    {
        public PersonTitles()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }

    }
}
