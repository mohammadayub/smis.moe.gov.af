using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Storage
{
    public class UploadedFile
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Encrypted { get; set; } = false;
    }
}
