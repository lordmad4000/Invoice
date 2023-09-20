using System.Globalization;
using System;

namespace SimplexInvoice.Application.Companies.Exceptions;

[Serializable]
public class CompanyRegisteringUpdatingException : Exception
{
    public CompanyRegisteringUpdatingException() : base()
    {
    }
    public CompanyRegisteringUpdatingException(string message) : base(message)
    {
    }
    public CompanyRegisteringUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}