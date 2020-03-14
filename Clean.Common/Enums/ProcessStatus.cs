using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class ProcessStatus
    {
        public static int InProcess { get; } = 1;
        public static int Processed { get; } = 2;
        public static int Rejected { get; } = 3;
    }
}
