using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class StockStatus
    {
        public static int Active { get; } = 1;
        public static int InActive { get; } = 2;
        public static int Completed { get; } = 3;
    }
}
