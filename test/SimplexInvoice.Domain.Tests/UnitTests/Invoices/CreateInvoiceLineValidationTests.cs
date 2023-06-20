using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class CreateInvoiceLineValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
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

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               0, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductCode_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductCode_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               null, 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductCode_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB___________________", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB",
                               null,
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductName_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA__________________________", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_ProductDescription_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_ProductDescription_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB",
                               "LASAÑA BOLOÑESA", 
                               null,
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void ProductDescription_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA__________________________", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Price_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               -2.60, 
                               "4%", 
                               4, 
                               10));
    }

    [Fact]
    public void Empty_TaxName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "", 
                               4, 
                               10));
    }

    [Fact]
    public void Null_TaxName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               null, 
                               4, 
                               10));
    }

    [Fact]
    public void TaxName_Length_Greather_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%___________________", 
                               4, 
                               10));
    }

    [Fact]
    public void TaxRate_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               -4, 
                               10));
    }

    [Fact]
    public void TaxRate_Greater_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               104, 
                               10));
    }

    [Fact]
    public void DiscountRate_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               -10));
    }

    [Fact]
    public void DiscountRate_Greater_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceLine.Create(Guid.NewGuid(), 
                               1, 
                               "LB", 
                               "LASAÑA BOLOÑESA", 
                               "LASAÑA BOLOÑESA", 
                               4, 
                               2.60, 
                               "4%", 
                               4, 
                               110));
    }

}
