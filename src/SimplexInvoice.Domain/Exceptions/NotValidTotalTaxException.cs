using System.Globalization;
using System;

namespace SimplexInvoice.Domain.Exceptions;

[Serializable]
public class NotValidTotalTaxException : Exception
{
    public NotValidTotalTaxException() : base()
    {
    }
    public NotValidTotalTaxException(string message) : base(message)
    {
    }
    public NotValidTotalTaxException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}