using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions
{
    [Serializable]
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException() : base()
        {
        }
        public BusinessRuleValidationException(string message) : base(message)
        {
        }
        public BusinessRuleValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }        
    }
}