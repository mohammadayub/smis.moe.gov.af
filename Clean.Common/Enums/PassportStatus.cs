using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class PassportStatus
    {
        public static int Active { get; } = 1;
        public static int InActive { get; } = 2;
        public static int BlackListed { get; } = 3;
        public static int Spoiled { get; } = 4;
    }
}
