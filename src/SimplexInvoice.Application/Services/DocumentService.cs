using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Interfaces;
using System;

namespace SimplexInvoice.Application.Services;
public class DocumentService : IDocumentService
{
    private readonly string DocumentNumberMask = "0000000000";

    public string GetNextInvoiceNumber(string lastNumber)
    {
        long result;
        if (!Int64.TryParse(lastNumber, out result))
            throw new BusinessRuleValidationException("Not valid Invoice Number.");

        result++;
        return result.ToString(DocumentNumberMask);
    }
}