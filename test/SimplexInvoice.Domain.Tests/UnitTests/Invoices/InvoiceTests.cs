using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class InvoiceTests : InvoiceSharedData
{
    [Fact]
    public void Remove_Line_By_Line_Number_Should_Be_True()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(1);

        //Assert
        Assert.True(lineBeenRemoved);
    }

    [Fact]
    public void Remove_Line_Should_Be_True()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        InvoiceLine invoiceLine = invoice.InvoiceLines.First();

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(invoiceLine);

        //Assert
        Assert.True(lineBeenRemoved);
    }

    [Fact]
    public void Remove_Non_Existent_Line_By_Line_Number_Should_Be_False()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(100);

        //Assert
        Assert.False(lineBeenRemoved);
    }

    [Fact]
    public void Remove_Non_Existent_Line_Should_Be_False()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        InvoiceLine invoiceLine = InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("EUR", 1.25), "0%", 0, 0);

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(invoiceLine);

        //Assert
        Assert.False(lineBeenRemoved);
    }

    [Fact]
    public void Remove_Last_Line_By_Line_Number_Should_Be_False()
    {
        //Arrange
        var invoice = GetInvoice();
        var invoiceLine = InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0);
        invoice.TryClearAndReplaceLines(new List<InvoiceLine> { invoiceLine });

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(1);

        //Assert
        Assert.False(lineBeenRemoved);
    }

    [Fact]
    public void Remove_Last_Line_Should_Be_False()
    {
        //Arrange
        var invoice = GetInvoice();
        var invoiceLine = InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0);
        invoice.TryClearAndReplaceLines(new List<InvoiceLine> { invoiceLine });

        //Act
        bool lineBeenRemoved = invoice.TryRemoveLine(invoiceLine);

        //Assert
        Assert.False(lineBeenRemoved);
    }

    [Fact]
    public void Add_Line_Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        var exception = Record.Exception(() =>
            invoice.AddLine(InvoiceLine.Create(Guid.NewGuid(), 6, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("EUR", 1.25), "0%", 0, 0)));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Add_Lines_Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        var exception = Record.Exception(() =>
            invoice.AddLines(new List<InvoiceLine>
            {
                InvoiceLine.Create(Guid.NewGuid(), 6, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("EUR", 1.25), "0%", 0, 0),
                InvoiceLine.Create(Guid.NewGuid(), 7, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0)
            }));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Add_Line_With_Distinct_Currency_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.AddLine(InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0)));
    }

    [Fact]
    public void Add_Lines_With_Distinct_Currency_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.AddLines(new List<InvoiceLine>
            {
                InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0),
                InvoiceLine.Create(Guid.NewGuid(), 2, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0)
            }));
    }

    [Fact]
    public void Clear_And_Replace_Lines_Should_Be_True()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        bool linesBeenReplaced = invoice.TryClearAndReplaceLines(
                                    new List<InvoiceLine>
                                    {
                                         InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("EUR", 1.25), "0%", 0, 0),
                                         InvoiceLine.Create(Guid.NewGuid(), 2, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0)
                                    });

        //Assert
        Assert.True(linesBeenReplaced);
    }

    [Fact]
    public void Clear_And_Replace_Lines_With_Distinct_Currency_Should_Be_False()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        bool linesBeenReplaced = invoice.TryClearAndReplaceLines(
                                    new List<InvoiceLine>
                                    {
                                         InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0),
                                         InvoiceLine.Create(Guid.NewGuid(), 2, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0)
                                    });

        //Assert
        Assert.False(linesBeenReplaced);
    }

    [Fact]
    public void Total_Taxes_Base_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedBaseAmount4 = 30.61;
        double expectedBaseAmount10 = 20.58;
        double expectedBaseAmount21 = 11.62;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());
        TotalTax invoiceTotalTaxes4 = invoice.TotalTaxes.First(c => c.Name.Equals("4%"));
        TotalTax invoiceTotalTaxes10 = invoice.TotalTaxes.First(c => c.Name.Equals("10%"));
        TotalTax invoiceTotalTaxes21 = invoice.TotalTaxes.First(c => c.Name.Equals("21%"));

        //Assert
        Assert.Equal(expectedBaseAmount4, invoiceTotalTaxes4.BaseAmount);
        Assert.Equal(expectedBaseAmount10, invoiceTotalTaxes10.BaseAmount);
        Assert.Equal(expectedBaseAmount21, invoiceTotalTaxes21.BaseAmount);
    }

    [Fact]
    public void Total_Taxes_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedAmount4 = 1.22;
        double expectedAmount10 = 2.06;
        double expectedAmount21 = 2.44;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());
        TotalTax invoiceTotalTaxes4 = invoice.TotalTaxes.First(c => c.Name.Equals("4%"));
        TotalTax invoiceTotalTaxes10 = invoice.TotalTaxes.First(c => c.Name.Equals("10%"));
        TotalTax invoiceTotalTaxes21 = invoice.TotalTaxes.First(c => c.Name.Equals("21%"));

        //Assert
        Assert.Equal(expectedAmount4, invoiceTotalTaxes4.Amount);
        Assert.Equal(expectedAmount10, invoiceTotalTaxes10.Amount);
        Assert.Equal(expectedAmount21, invoiceTotalTaxes21.Amount);
    }

    [Fact]
    public void Total_Tax_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedTotalTaxAmount = 5.73;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());

        //Assert
        Assert.Equal(expectedTotalTaxAmount, invoice.TotalTax.Amount);
    }

    [Fact]
    public void Total_Discount_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedTotalDiscountAmount = 2.84;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());

        //Assert
        Assert.Equal(expectedTotalDiscountAmount, invoice.TotalDiscount.Amount);
    }

    [Fact]
    public void Total_Tax_Base_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedTotalTaxBaseAmount = 79.31;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());

        //Assert
        Assert.Equal(expectedTotalTaxBaseAmount, invoice.TotalTaxBase.Amount);
    }

    [Fact]
    public void Total_Amount_Should_Be_As_Expected()
    {
        //Arrange
        Invoice invoice = GetInvoice();
        double expectedTotalAmount = 82.2;

        //Act
        invoice.TryClearAndReplaceLines(GetTotalsTestsInvoiceLines());

        //Assert
        Assert.Equal(expectedTotalAmount, invoice.Total.Amount);
    }

}