using System.Globalization;
using System;

namespace SimplexInvoice.Application.Companies.Exceptions;

[Serializable]
public class CompanyRegisteringException : Exception
{
    public CompanyRegisteringException() : base()
    {
    }
    public CompanyRegisteringException(string message) : base(message)
    {
    }
    public CompanyRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}