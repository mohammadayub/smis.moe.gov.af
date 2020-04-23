using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clean.Common.Service
{
    public class MRZHelper
    {
        private static readonly Dictionary<string, int> CharCodes = new Dictionary<string, int>
        {
            ["<"] = 0,
            ["0"] = 0, 
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9,
            ["A"] = 10,
            ["B"] = 11,
            ["C"] = 12,
            ["D"] = 13,
            ["E"] = 14,
            ["F"] = 15,
            ["G"] = 16,
            ["H"] = 17,
            ["I"] = 18,
            ["J"] = 19,
            ["K"] = 20,
            ["L"] = 21,
            ["M"] = 22,
            ["N"] = 23,
            ["O"] = 24,
            ["P"] = 25,
            ["Q"] = 26,
            ["R"] = 27,
            ["S"] = 28,
            ["T"] = 29,
            ["U"] = 30,
            ["V"] = 31,
            ["W"] = 32,
            ["X"] = 33,
            ["Y"] = 34,
            ["Z"] = 35
        };
        public static int MRZCharacterCount { get; } = 44;
        public static string GenerateFirstLine(string name,string familyname,string passporttype)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(passporttype.ToUpper());
            sb.Append(AppConfig.NationalCode.ToUpper());
            sb.Append(familyname.ToUpper().Replace(" ","<"));
            sb.Append("<<");
            sb.Append(name.ToUpper().Replace(" ", "<"));
            int length = MRZCharacterCount - sb.Length;
            sb.Append(Enumerable.Repeat("<", length).Aggregate((a, b) => a + b));
            return sb.ToString();
        }

        public static string GenerateSecondLine(string passportNumber,DateTime DateOfBirth,string GenderCode,DateTime ExpiryDate)
        {
            StringBuilder sb = new StringBuilder();
            if(passportNumber.Length < 9)
            {
                while (passportNumber.Length < 9)
                    passportNumber += "<";
            }
            sb.Append(passportNumber);
            sb.Append(MRZHelper.CheckDigit(passportNumber));
            sb.Append(AppConfig.NationalCode.ToUpper());
            sb.Append(DateOfBirth.ToString("yyMMdd"));
            sb.Append(MRZHelper.CheckDigit(DateOfBirth.ToString("yyMMdd")));
            sb.Append(GenderCode);
            sb.Append(ExpiryDate.ToString("yyMMdd"));
            sb.Append(MRZHelper.CheckDigit(ExpiryDate.ToString("yyMMdd")));
            int length = MRZCharacterCount - sb.Length - 1;
            sb.Append(Enumerable.Repeat("<", length).Aggregate((a, b) => a + b));
            sb.Append(MRZHelper.CheckDigit(
                passportNumber +
                MRZHelper.CheckDigit(passportNumber) +
                DateOfBirth.ToString("yyMMdd") +
                MRZHelper.CheckDigit(DateOfBirth.ToString("yyMMdd")) +
                ExpiryDate.ToString("yyMMdd") +
                MRZHelper.CheckDigit(ExpiryDate.ToString("yyMMdd"))
                ));
            return sb.ToString();
        }

        private static int CheckDigit(string code)
        {
            int TotalSum = 0;
            int[] W = new int[] { 7, 3, 1 };
            for(int i = 0; i < code.Length; i+=1)
            {
                var cur = code[i].ToString();
                var WVal = W[i % 3];
                var CVal = CharCodes[cur];
                TotalSum += CVal * WVal;
            }
            return TotalSum % 10;
        }
    }
}
