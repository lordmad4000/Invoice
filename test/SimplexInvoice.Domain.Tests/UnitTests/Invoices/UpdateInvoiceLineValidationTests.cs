using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class UpdateInvoiceLineValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act
        var exception = Record.Exception(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void LineNumber_Lesser_Than_1_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductCode_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductCode_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               null, 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductCode_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB___________________", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB",
                               null,
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductName_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA__________________________", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductDescription_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductDescription_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB",
                               "LASAÑA BOLOÑESA", 
                               null,
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductDescription_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA__________________________", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void PriceAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               -2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_TaxName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_TaxName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               null, 
                               4, 
                               10));
    }

    [Fact]
    public void TaxName_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%___________________", 
                               4, 
                               10));
    }

    [Fact]
    public void TaxRate_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               -4, 
                               10));
    }

    [Fact]
    public void TaxRate_Greater_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               104, 
                               10));
    }

    [Fact]
    public void DiscountRate_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               -10));
    }

    [Fact]
    public void DiscountRate_Greater_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        InvoiceLine invoiceLine = GetInvoiceLine();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoiceLine.Update(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "EUR", 
                               "4%", 
                               4, 
                               110));
    }

    private static InvoiceLine GetInvoiceLine() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "FU", 
                               "FAIRY ULTRA", 
                               "FAIRY ULTRA", 
                               2, 
                               3.25, 
                               "USD", 
                               "21%", 
                               21, 
                               5);

}
