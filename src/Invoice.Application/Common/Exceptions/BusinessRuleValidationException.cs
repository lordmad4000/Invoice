using System.Globalization;
using System;

namespace Invoice.Application.Common.Exceptions
{
    [Serializable]
    public class RegisterRuleValidationException : Exception
    {
        public RegisterRuleValidationException() : base()
        {
        }
        public RegisterRuleValidationException(string message) : base(message)
        {
        }
        public RegisterRuleValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }        
    }
}