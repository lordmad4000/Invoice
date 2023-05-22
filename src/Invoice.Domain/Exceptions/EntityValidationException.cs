using System.Globalization;
using System;

namespace Invoice.Domain.Exceptions
{
    [Serializable]
    public class EntityValidationException : Exception
    {
        public EntityValidationException() : base()
        {
        }
        public EntityValidationException(string message) : base(message)
        {
        }
        public EntityValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }        
    }
}