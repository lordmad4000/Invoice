using System.Globalization;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Exceptions;

[Serializable]
public class IdDocumentTypeRegisteringException : Exception
{
    public IdDocumentTypeRegisteringException() : base()
    {
    }
    public IdDocumentTypeRegisteringException(string message) : base(message)
    {
    }
    public IdDocumentTypeRegisteringException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}