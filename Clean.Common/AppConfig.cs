using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Clean.Common
{
    public class AppConfig
    {
        private static IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");

        private static IConfigurationRoot AppSettings
        {
            get
            {
                return builder.Build();
            }
        }

        public static string JWTSecret
        {
            get
            {
                return AppSettings["JWTSecret"];
            }
        }
        public static double JWTExpireDays
        {
            get
            {
                return AppSettings.GetValue<Double>("JWTExpireDays");
            }
        }
        public static string JWTIssuer
        {
            get
            {
                return AppSettings.GetValue<String>("JWTIssuer");
            }
        }

        public static string BaseConnectionString
        {
            get
            {
                return AppSettings["BaseConnectionString"];
            }
        }

        public static bool EncryptionEnabled
        {
            get
            {
                return false;
            }
        }
        
        public static string ImagesPath
        {
            get
            {
                return AppSettings["ImagesPath"];
            }
        }
        public static string SignaturesPath
        {
            get
            {
                return AppSettings["SignaturesPath"];
            }
        }
        public static string DocumentsPath
        {
            get
            {
                return AppSettings["DocumentsPath"];
            }
        }

        public static string IdentityConnectionString
        {
            get
            {
                return AppSettings["IdentityConnectionString"];
            }
        }

        public static string ApplicationTitle
        {
            get
            {
                return AppSettings["AppName"];
            }
        }


        public static string ApplicationLogo
        {
            get
            {
                return AppSettings["AppLogo"];
            }
        }

        public static string Organization
        {
            get
            {
                return AppSettings["Organization"];
            }
        }

        public static string ExcelFormatsPath
        {
            get
            {
                return AppSettings["Formats"];
            }
        }
        public static string SystemPhotosPath
        {
            get
            {
                return AppSettings["Photo"];
            }
        }

        public static string CardPath
        {
            get
            {
                return AppSettings["Card"];
            }
        }
        public static string M40
        {
            get
            {
                return AppSettings["M40"];
            }
        }

        public static bool ShowStackTrace
        {
            get
            {
                switch (AppSettings["ShowStackTrace"].ToString())
                {
                    case "false":
                        return false;
                    case "true":
                        return true;
                    default:
                        return false;
                }
            }
        }

        public static string NationalCode
        {
            get
            {
                return AppSettings["NationalCode"];
            }
        }
        

        public static string AttachmentsPath
        {
            get
            {
                return AppSettings["AttachmentsPath"];
            }
        }
    }
}
