using System.Globalization;
using System;

namespace Invoice.Infra.Exceptions
{
    [Serializable]
    public class CacheException : Exception
    {
       public CacheException() : base()
       {
       }
       public CacheException(string message) : base(message)
       {
       }
       public CacheException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
       {
       }        
    }
}