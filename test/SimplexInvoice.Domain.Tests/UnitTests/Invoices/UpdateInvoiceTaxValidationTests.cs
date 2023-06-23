using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class UpdateInvoiceTaxValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", 12.24, "USD");

        //Act
        var exception = Record.Exception(() =>
            invoice.Update("4%", 7.00, "EUR"));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", 12.24, "USD");

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("", 7.00, "EUR"));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", 12.24, "USD");

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update(null, 7.00, "EUR"));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", 12.24, "USD");

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("4%___________________", 7.00, "EUR"));

    }

    [Fact]
    public void TotalAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var invoice = InvoiceTax.Create("10%", 12.24, "USD");

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("4%", -2.00, "EUR"));
    }

}
