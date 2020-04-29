using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Enums
{
    public class StatusTypes
    {
        public static string ProcessTracking { get; } = "PTS";
        public static string PassportStock { get; } = "PST";
        public static string PassportPrint { get; } = "PSP";
        public static string Passport { get; } = "PAS";
        public static string Application { get; } = "APS";
    }
}
