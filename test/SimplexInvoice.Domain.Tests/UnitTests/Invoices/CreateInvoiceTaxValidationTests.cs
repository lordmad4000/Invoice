using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
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
            InvoiceTax.Create("10%", 12.24, "USD"));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("", 12.24, "USD"));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create(null, 12.24, "USD"));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("10%__________________", 12.24, "USD"));
    }

    [Fact]
    public void TotalAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            InvoiceTax.Create("10%", -5.12, "USD"));
    }

}
