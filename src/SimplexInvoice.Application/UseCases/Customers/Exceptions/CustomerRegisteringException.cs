using System.Globalization;
using System;

namespace SimplexInvoice.Application.Customers.Exceptions;

[Serializable]
public class CustomerRegisteringException : Exception
{
    public CustomerRegisteringException() : base()
    {
    }
    public CustomerRegisteringException(string message) : base(message)
    {
    }
    public CustomerRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}