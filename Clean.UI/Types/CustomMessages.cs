using Clean.Common;
using Clean.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.UI.Types
{
    public class CustomMessages
    {
        public static string InternalSystemException { get; private set; } = "کاربر محترم درخواست شما مواجه با مشکل تخنیکی میباشد. لطفا با مسئول سیستم به تماس شوید";
        public static string ValidationExceptionTitle { get; private set; } = "اطلاعات فورم درست نمیباشد";
        public static string BusinessRuleExceptionTitle { get; private set; } = "کوشش خلاف اصول";
        public static string IdentityExceptionMessage { get; private set; } = "خطا در هویت";
        public static string FileNotFoundExceptionTitle { get; private set; } = "عدم دریافت فایل";
        public static string StateExceptionTitle(Exception ex)
        {
            Type exceptionType = ex.GetType();
            StringBuilder ExceptionTitleBuilder = new StringBuilder();
            if (exceptionType.Equals(typeof(ValidationException)))
            {
                ExceptionTitleBuilder.Append(ValidationExceptionTitle);
            }
            else if (exceptionType.Equals(typeof(BusinessRulesException)))
            {
                ExceptionTitleBuilder.Append(BusinessRuleExceptionTitle);
            }
            else if (exceptionType.Equals(typeof(IdentityException)))
            {
                ExceptionTitleBuilder.Append(IdentityExceptionMessage);
            }
            else if (exceptionType.Equals(typeof(FileNotFoundException)))
            {
                ExceptionTitleBuilder.Append(FileNotFoundExceptionTitle);
            }
            else
            {
                ExceptionTitleBuilder.Append(InternalSystemException);
            }
            return ExceptionTitleBuilder.ToString();
        }


        public static string DescribeException(Exception ex)
        {
            bool ShowStackTrace = true;
            StringBuilder DescriptionBuilder = new StringBuilder();
            ShowStackTrace = Convert.ToBoolean(AppConfig.ShowStackTrace);
            List<string> CustomMessageExceptions = new List<string>();
            CustomMessageExceptions.Add("BusinessRulesException");
            CustomMessageExceptions.Add("ValidationException");
            CustomMessageExceptions.Add("IdentityException");
            CustomMessageExceptions.Add("FileNotFoundException");
            if (ShowStackTrace && !CustomMessageExceptions.Contains(ex.GetType().Name))
            {

                DescriptionBuilder
                             .Append("Message: ")
                             .Append("\n")
                             .Append(ex.Message)
                             .Append("Inner Exception:")
                             .Append("\n")
                             .Append(ex.InnerException != null ? ex.InnerException.Message.ToString() : string.Empty)
                             .Append("Stack Trace:")
                             .Append("\n")
                             .Append(ex.StackTrace);
            }
            else
            {
                DescriptionBuilder.Append("\n").Append(ex.Message);
                if(ex.InnerException != null)
                {
                    DescriptionBuilder.Append("\n INEX : ").Append(ex.InnerException.Message);
                }
            }

            return DescriptionBuilder.ToString();

        }




        public static UIResult FabricateException(Exception ex)
        {
            return new UIResult()
            {
                Data = null,
                Status = UIStatus.Failure,
                Text = StateExceptionTitle(ex),
                Description = DescribeException(ex)
            };
        }


        // Handling Get File Exceptions
        public static int GetStatusCode(Exception ex)
        {
            Type exceptionType = ex.GetType();
            int StatusCode;
            if (exceptionType.Equals(typeof(ValidationException)))
            {
                StatusCode = 400; // Bad Request 
            }
            else if (exceptionType.Equals(typeof(BusinessRulesException)))
            {
                StatusCode = 428; // Pre condition required
            }
            else if (exceptionType.Equals(typeof(IdentityException)))
            {
                StatusCode = 401; // unauthorized
            }
            else if (exceptionType.Equals(typeof(FileNotFoundException)))
            {
                StatusCode = 404; // NotFound
            }
            else
            {
                StatusCode = 500; // Server error
            }

            return StatusCode;
        }
        public static object GetMessage(Exception ex)
        {
            return new { msg = DescribeException(ex), title = StateExceptionTitle(ex) };
        }
    }
}
