using System.Globalization;
using System;

namespace SimplexInvoice.Application.Products.Exceptions;

[Serializable]
public class ProductUpdatingException : Exception
{
    public ProductUpdatingException() : base()
    {
    }
    public ProductUpdatingException(string message) : base(message)
    {
    }
    public ProductUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}