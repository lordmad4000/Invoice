using System.Globalization;
using System;

namespace SimplexInvoice.Application.Customers.Exceptions;

[Serializable]
public class CustomerUpdatingException : Exception
{
    public CustomerUpdatingException() : base()
    {
    }
    public CustomerUpdatingException(string message) : base(message)
    {
    }
    public CustomerUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}