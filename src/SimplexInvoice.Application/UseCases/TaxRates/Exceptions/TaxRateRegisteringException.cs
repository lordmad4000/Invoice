using System.Globalization;
using System;

namespace SimplexInvoice.Application.TaxRates.Exceptions;

[Serializable]
public class TaxRateRegisteringException : Exception
{
    public TaxRateRegisteringException() : base()
    {
    }
    public TaxRateRegisteringException(string message) : base(message)
    {
    }
    public TaxRateRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}