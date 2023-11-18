namespace SimplexInvoice.Domain.Interfaces;

public interface IDocumentService
{
    string GetNextInvoiceNumber(string lastNumber);
}