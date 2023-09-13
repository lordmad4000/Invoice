using System.Globalization;
using System;

namespace SimplexInvoice.Application.Customers.Exceptions;

[Serializable]
public class CustomerRemovingException : Exception
{
    public CustomerRemovingException() : base()
    {
    }
    public CustomerRemovingException(string message) : base(message)
    {
    }
    public CustomerRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}