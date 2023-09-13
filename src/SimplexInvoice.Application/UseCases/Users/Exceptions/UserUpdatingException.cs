using System.Globalization;
using System;

namespace SimplexInvoice.Application.Users.Exceptions;

[Serializable]
public class UserUpdatingException : Exception
{
    public UserUpdatingException() : base()
    {
    }
    public UserUpdatingException(string message) : base(message)
    {
    }
    public UserUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}