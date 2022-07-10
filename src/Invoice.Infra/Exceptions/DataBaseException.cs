using System;

namespace Invoice.Infra.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {
        }
    }
}