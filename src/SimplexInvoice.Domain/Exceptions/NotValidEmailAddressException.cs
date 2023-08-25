using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions
{
    [Serializable]
    public class NotValidEmailAddressException : Exception
    {
        public NotValidEmailAddressException() : base()
        {
        }
        public NotValidEmailAddressException(string message) : base(message)
        {
        }
        public NotValidEmailAddressException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}