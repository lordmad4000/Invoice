using System;

namespace Users.Infra.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {
        }
    }
}