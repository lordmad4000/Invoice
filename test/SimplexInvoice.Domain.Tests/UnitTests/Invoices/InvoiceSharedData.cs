using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class InvoiceSharedData
{
    protected Invoice GetInvoice() =>
        Invoice.Create("1",
                       "Test Invoice",
                       "Company Test",
                       "CIF",
                       "A37610482",
                       new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                       new PhoneNumber("+01 718 222 2222"),
                       new EmailAddress("prueba@gmail.com"),
                       "Customer Test",
                       "NIF",
                       "36184297R",
                       new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                       new PhoneNumber("+01 718 222 2222"),
                       new EmailAddress("prueba@gmail.com"),
                       new List<InvoiceLine>()
                       {
                            InvoiceLine.Create(Guid.NewGuid(), 1, "LB",  "LASAÑA BOLOÑESA",      "LASAÑA BOLOÑESA",      1, 1,     new Money("EUR", 2.60), "10%", 10, 0),
                            InvoiceLine.Create(Guid.NewGuid(), 3, "BNN", "BANANA",               "BANANA",               1, 1.628, new Money("EUR", 1.25), "0%",   0, 0),
                            InvoiceLine.Create(Guid.NewGuid(), 4, "VFU", "VAJILLAS FAIRY ULTRA", "VAJILLAS FAIRY ULTRA", 2, 2,     new Money("EUR", 3.25), "21%", 21, 0),
                            InvoiceLine.Create(Guid.NewGuid(), 5, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 3, 3,     new Money("EUR", 0.95), "4%",   4, 0),
                       });

    protected List<InvoiceLine> GetInvoiceLines() =>
        new List<InvoiceLine>
        {
            InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("EUR", 1.25), "0%", 0, 0),
            InvoiceLine.Create(Guid.NewGuid(), 2, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0),
            InvoiceLine.Create(Guid.NewGuid(), 3, "ARA", "ARANDANO", "ARANDANO", 2, 16, new Money("EUR", 1.50), "4%", 4, 0),
            InvoiceLine.Create(Guid.NewGuid(), 4, "LB", "LASAÑA BOLOÑESA", "LASAÑA BOLOÑESA", 4, 4, new Money("EUR", 2.60), "10%", 10, 0),
            InvoiceLine.Create(Guid.NewGuid(), 5, "VFU", "VAJILLAS FAIRY ULTRA", "VAJILLAS FAIRY ULTRA", 3, 3, new Money("EUR", 3.25), "21%", 21, 0),
        };

    protected List<InvoiceLine> GetTotalsTestsInvoiceLines() => 
        new List<InvoiceLine>
        {
            InvoiceLine.Create(Guid.NewGuid(), 1, "P1", "PRODUCT 1", "PRODUCT 1", 1, 5.650, new Money("EUR", 2.59), "10%", 10, 5),
            InvoiceLine.Create(Guid.NewGuid(), 2, "P2", "PRODUCT 2", "PRODUCT 2", 1, 2.100, new Money("EUR", 1.29), "0%",   0, 0),
            InvoiceLine.Create(Guid.NewGuid(), 3, "P3", "PRODUCT 3", "PRODUCT 3", 1, 3.000, new Money("EUR", 3.29), "21%", 21, 0),
            InvoiceLine.Create(Guid.NewGuid(), 4, "P4", "PRODUCT 4", "PRODUCT 4", 1, 4.250, new Money("EUR", 0.99), "4%",   4, 0),
            InvoiceLine.Create(Guid.NewGuid(), 5, "P5", "PRODUCT 5", "PRODUCT 5", 1, 5.000, new Money("EUR", 1.19), "10%", 10, 0),
            InvoiceLine.Create(Guid.NewGuid(), 6, "P6", "PRODUCT 6", "PRODUCT 6", 1, 6.128, new Money("EUR", 2.25), "0%",   0, 0),
            InvoiceLine.Create(Guid.NewGuid(), 7, "P7", "PRODUCT 7", "PRODUCT 7", 1, 7.000, new Money("EUR", 0.25), "21%", 21, 0),
            InvoiceLine.Create(Guid.NewGuid(), 8, "P8", "PRODUCT 8", "PRODUCT 8", 1, 8.000, new Money("EUR", 3.30), "4%",   4, 8),
        };

}