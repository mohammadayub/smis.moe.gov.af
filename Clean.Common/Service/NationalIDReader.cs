using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Service
{
    public class NationalIDReader
    {
        private static Dictionary<int, string> JuldTypes = new Dictionary<int, string>
        {
            [1] = "قلم انداز",
            [2] = "متفرقه",
            [3] = "اصل اساس",
            [4] = "تولدات"
        };
        public static string ConvertJSONToShortString(String JSON, String Type)
        {
            if (!string.IsNullOrEmpty(JSON))
            {
                dynamic item = JsonConvert.DeserializeObject(JSON);
                string result = "";// Type + " | ";

                if (!String.IsNullOrEmpty(Convert.ToString(item["JT"])))
                {
                    int JN = Convert.ToInt32(item["JT"]);
                    string sjt = JuldTypes[JN].Substring(0, 1);

                    string JY = Convert.ToString(item["JY"]);
                    string sjy = JY.Substring(2, 2);
                    result += "ج";
                    if (!String.IsNullOrEmpty(Convert.ToString(item["JN"]))) result = result + item["JN"] + "-";
                    if (!String.IsNullOrEmpty(Convert.ToString(item["JT"]))) result = result + sjt + "-";
                    if (!String.IsNullOrEmpty(Convert.ToString(item["JY"]))) result = result + sjy + "-";
                    if (!String.IsNullOrEmpty(Convert.ToString(item["P"]))) result = result + "ص" + item["P"] + "-";
                    if (!String.IsNullOrEmpty(Convert.ToString(item["N"]))) result = result + "ث" + item["N"] + "-";

                    if (!String.IsNullOrEmpty(Convert.ToString(item["S"]))) result = result + "صکوک" + item["S"] + "-";


                }
                else
                {
                    result = result + item["S"] + "-";
                }

                int last = result.LastIndexOf('-');

                return result.Remove(last);
            }
            else
            {
                return null;
            }

        }

        public static string ConvertJSONToString(String JSON, String Type)
        {
            if (!string.IsNullOrEmpty(JSON))
            {
                dynamic item = JsonConvert.DeserializeObject(JSON);
                string result = Type + " | ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["S"]))) result = result + "نمبر سند هویت: " + item["S"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["JN"]))) result = result + "جلد: " +  item["JN"] + " ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["JT"]))) result = result + JuldTypes[Convert.ToInt32(item["JT"])] + " ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["JY"]))) result = result + "سال " + item["JY"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["P"]))) result = result + "صفحه: " + item["P"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["N"]))) result = result + "نمبر ثبت: " + item["N"] + "، ";
                if (!String.IsNullOrEmpty(Convert.ToString(item["Y"]))) result = result + "سال: " + item["Y"] + "، ";

                int last = result.LastIndexOf('،');
                return result.Remove(last);
            }
            else
            {
                return null;
            }

        }


        public static string GetTazkiraNumber(String JSON, String Type)
        {
            if (!string.IsNullOrEmpty(JSON))
            {
                dynamic item = JsonConvert.DeserializeObject(JSON);
                string result = "";
                if (!String.IsNullOrEmpty(Convert.ToString(item["S"]))) result = item["S"];
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
