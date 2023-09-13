using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions
{
    [Serializable]
    public class NotValidMoneyException : Exception
    {
        public NotValidMoneyException() : base()
        {
        }
        public NotValidMoneyException(string message) : base(message)
        {
        }
        public NotValidMoneyException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}