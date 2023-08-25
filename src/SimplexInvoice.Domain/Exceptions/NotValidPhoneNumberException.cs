using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions
{
    [Serializable]
    public class NotValidPhoneNumberException : Exception
    {
        public NotValidPhoneNumberException() : base()
        {
        }
        public NotValidPhoneNumberException(string message) : base(message)
        {
        }
        public NotValidPhoneNumberException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}