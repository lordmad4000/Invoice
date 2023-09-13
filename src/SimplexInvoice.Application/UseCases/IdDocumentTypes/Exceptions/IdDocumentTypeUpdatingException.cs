using System.Globalization;
using System;

namespace SimplexInvoice.Application.IdDocumentTypes.Exceptions;

[Serializable]
public class IdDocumentTypeUpdatingException : Exception
{
    public IdDocumentTypeUpdatingException() : base()
    {
    }
    public IdDocumentTypeUpdatingException(string message) : base(message)
    {
    }
    public IdDocumentTypeUpdatingException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}