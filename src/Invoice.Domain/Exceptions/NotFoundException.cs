using System.Globalization;
using System;

namespace Invoice.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}