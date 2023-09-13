using System.Globalization;
using System;

namespace SimplexInvoice.Application.Products.Exceptions;

[Serializable]
public class ProductRemovingException : Exception
{
    public ProductRemovingException() : base()
    {
    }
    public ProductRemovingException(string message) : base(message)
    {
    }
    public ProductRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}