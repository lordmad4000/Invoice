using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class UpdateInvoiceTaxValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", new Money("USD", 12.24));

        //Act
        var exception = Record.Exception(() =>
            invoice.Update("4%", new Money("EUR", 7.00)));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", new Money("USD", 12.24));

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("", new Money("EUR", 7.00)));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", new Money("USD", 12.24));

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update(null, new Money("EUR", 7.00)));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", new Money("USD", 12.24));

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("4%___________________", new Money("EUR", 7.00)));

    }

    [Fact]
    public void TotalAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", new Money("USD", 12.24));

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("4%", new Money("EUR", -2.00)));
    }

}
