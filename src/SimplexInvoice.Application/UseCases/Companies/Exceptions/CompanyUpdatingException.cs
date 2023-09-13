using System.Globalization;
using System;

namespace SimplexInvoice.Application.Companies.Exceptions;

[Serializable]
public class CompanyUpdatingException : Exception
{
    public CompanyUpdatingException() : base()
    {
    }
    public CompanyUpdatingException(string message) : base(message)
    {
    }
    public CompanyUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}