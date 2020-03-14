using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Exceptions
{
    public class InternalSystemException:Exception
    {
        public InternalSystemException(string Message) : base(Message)
        {
        }
        public InternalSystemException()
        {
        }
        public string MessageDetails { get; set; }
        public string StackTraceDetails { get; set; }
        public Exception InnerExceptionDetails { get; set; }
    }
}
