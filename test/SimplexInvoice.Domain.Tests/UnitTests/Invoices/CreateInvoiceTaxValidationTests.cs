using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class CreateInvoiceTaxValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() =>
            InvoiceTax.Create("10%", new Money("USD", 12.24)));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("", new Money("USD", 12.24)));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create(null, new Money("USD", 12.24)));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("10%__________________", new Money("USD", 12.24)));
    }

    [Fact]
    public void TotalAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("10%", new Money("USD", -5.12)));
    }

}
