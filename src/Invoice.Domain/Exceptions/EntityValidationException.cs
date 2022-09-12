using System;

namespace Invoice.Domain.Exceptions
{
    [Serializable]
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message) : base(message)
        {
        }
    }
}