using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions
{
    [Serializable]
    public class NotValidAddressException : Exception
    {
        public NotValidAddressException() : base()
        {
        }
        public NotValidAddressException(string message) : base(message)
        {
        }
        public NotValidAddressException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}