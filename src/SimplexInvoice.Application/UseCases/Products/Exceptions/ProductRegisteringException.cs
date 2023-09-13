using System.Globalization;
using System;

namespace SimplexInvoice.Application.Products.Exceptions;

[Serializable]
public class ProductRegisteringException : Exception
{
    public ProductRegisteringException() : base()
    {
    }
    public ProductRegisteringException(string message) : base(message)
    {
    }
    public ProductRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}