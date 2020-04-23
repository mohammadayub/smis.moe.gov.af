using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Common.Extensions
{
    public static class StringExtensions
    {
        private static string EncryptionKey = "mofa-p@3390rt-P@33word-2020-+++";
        public static string Right(this string sValue, int noOfExtraction)
        {
            return sValue.Substring(sValue.Length - noOfExtraction);
        }

        public static byte[] GetBytes(this string cur)
        {
            return System.Text.Encoding.UTF8.GetBytes(cur);
        }


        public static string CleanValue(this string val)
        {
            return !string.IsNullOrWhiteSpace(val) ? val.Trim() : "درج نگردیده";
        }
        public static string EncryptID(this string Value)
        {
            string result = String.Empty;
            byte[] clearBytes = Encoding.Unicode.GetBytes(Value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    result = Convert.ToBase64String(ms.ToArray());
                }
            }
            return result;
        }

        public static string DecryptID(this string Value)
        {
            string replica = Value.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(replica);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    replica = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return replica;
        }

        public static string EnglishToPersian(this string persianStr)
        {
            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["0"] = "۰",
                ["1"] = "۱",
                ["2"] = "۲",
                ["3"] = "۳",
                ["4"] = "۴",
                ["5"] = "۵",
                ["6"] = "۶",
                ["7"] = "۷",
                ["8"] = "۸",
                ["9"] = "۹"
            };
            return LettersDictionary.Aggregate(persianStr, (current, item) =>
                         current.Replace(item.Key, item.Value));
        }
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9"
            };
            return LettersDictionary.Aggregate(persianStr, (current, item) =>
                         current.Replace(item.Key, item.Value));
        }
    }
}
