using System;

namespace Invoice.Domain.Exceptions
{
    [Serializable]
    public class NotValidEmailAddressException : Exception
    {
        public NotValidEmailAddressException(string message) : base(message)
        {
        }
    }
}