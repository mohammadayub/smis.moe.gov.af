using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class SystemProcess
    {
        public static int Registration { get; } = 1;
        public static int Biometric { get; } = 2;
        public static int ReasearchAndControl { get; } = 3;
        public static int Authorization { get; } = 4;
        public static int Print { get; } = 5;
        public static int QualityControl { get; } = 6;
        public static int Close { get; } = 7;
    }
}
