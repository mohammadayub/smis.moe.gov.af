using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class PassportPrintStatus
    {
        public static int Registered { get; } = 1;
        public static int Printed { get; } = 2;
        public static int Spoiled { get; } = 3;
    }
}
