using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class SystemRoles
    {
        public static string Admin { get; } = "Administrator";
        public static string DataEntry { get; } = "Data-Entry";
        public static string ResearchAndControl { get; } = "Research";
        public static string Authorization { get; } = "Authorization";
        public static string Print { get; } = "Print";
        public static string QualityControl { get; } = "Quality-Control";
    }
}
