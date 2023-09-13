using System.Globalization;
using System;

namespace SimplexInvoice.Application.TaxRates.Exceptions;

[Serializable]
public class TaxRateRemovingException : Exception
{
    public TaxRateRemovingException() : base()
    {
    }
    public TaxRateRemovingException(string message) : base(message)
    {
    }
    public TaxRateRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}