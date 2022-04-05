using System;

namespace Users.Domain.Exceptions
{
    [Serializable]
    public class NotValidEmailAddressException : Exception
    {
        public NotValidEmailAddressException(string message) : base(message)
        {
        }
    }
}