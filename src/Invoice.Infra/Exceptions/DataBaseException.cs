using System.Globalization;
using System;

namespace Invoice.Infra.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
       public DataBaseException() : base()
        {
        }
        public DataBaseException(string message) : base(message)
        {
        }
        public DataBaseException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }        
    }
}