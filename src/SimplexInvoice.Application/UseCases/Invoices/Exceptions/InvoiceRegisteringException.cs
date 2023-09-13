using System.Globalization;
using System;

namespace SimplexInvoice.Application.Invoices.Exceptions;

[Serializable]
public class InvoiceRegisteringException : Exception
{
    public InvoiceRegisteringException() : base()
    {
    }
    public InvoiceRegisteringException(string message) : base(message)
    {
    }
    public InvoiceRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}