using System.Globalization;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Exceptions;

[Serializable]
public class IdDocumentTypeRemovingException : Exception
{
    public IdDocumentTypeRemovingException() : base()
    {
    }
    public IdDocumentTypeRemovingException(string message) : base(message)
    {
    }
    public IdDocumentTypeRemovingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}