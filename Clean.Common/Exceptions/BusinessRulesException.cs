using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Common.Exceptions
{
    public class BusinessRulesException : Exception
    {
        public BusinessRulesException(string Message) : base(Message)
        {

        }
    }
}
