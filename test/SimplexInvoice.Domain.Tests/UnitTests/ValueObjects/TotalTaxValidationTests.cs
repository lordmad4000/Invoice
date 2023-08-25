using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class TotalTaxValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() => new TotalTax("4%", 30.40, 1.22));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_NotValidTotalTaxException()
    {
        //Arrange            

        //Act & Assert
        Assert.Throws<NotValidTotalTaxException>(() => new TotalTax("", 30.40, 1.22));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_NotValidTotalTaxException()
    {
        //Arrange            

        //Act & Assert
        Assert.Throws<NotValidTotalTaxException>(() => new TotalTax(null, 30.40, 1.22));
    }

    [Fact]
    public void Name_Length_Greather_Than_20_Should_Be_Throw_NotValidTotalTaxException()
    {
        //Arrange            

        //Act & Assert
        Assert.Throws<NotValidTotalTaxException>(() => new TotalTax("10%__________________", 30.40, 1.22));
    }

}
