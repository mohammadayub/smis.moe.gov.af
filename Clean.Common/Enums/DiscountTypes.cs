using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class DiscountTypes
    {
        public static string Percentage { get; } = "PR";
        public static string WholePrice { get; } = "WL";
        public static string DiscountPrice { get; } = "PP";
    }
}
