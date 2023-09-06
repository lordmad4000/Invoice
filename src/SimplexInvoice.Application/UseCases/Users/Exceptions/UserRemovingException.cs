using System.Globalization;
using System;

namespace SimplexInvoice.Application.Users.Exceptions;

[Serializable]
public class UserRemovingException : Exception
{
    public UserRemovingException() : base()
    {
    }
    public UserRemovingException(string message) : base(message)
    {
    }
    public UserRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}