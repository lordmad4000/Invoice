using System.Globalization;
using System;

namespace SimplexInvoice.Application.Companies.Exceptions;

[Serializable]
public class CompanyRemovingException : Exception
{
    public CompanyRemovingException() : base()
    {
    }
    public CompanyRemovingException(string message) : base(message)
    {
    }
    public CompanyRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}