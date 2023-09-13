using System.Globalization;
using System;

namespace SimplexInvoice.Application.Users.Exceptions;

[Serializable]
public class UserRegisteringException : Exception
{
    public UserRegisteringException() : base()
    {
    }
    public UserRegisteringException(string message) : base(message)
    {
    }
    public UserRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}