using System.Globalization;
using System;

namespace SimplexInvoice.Application.TaxRates.Exceptions;

[Serializable]
public class TaxRateUpdatingException : Exception
{
    public TaxRateUpdatingException() : base()
    {
    }
    public TaxRateUpdatingException(string message) : base(message)
    {
    }
    public TaxRateUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}