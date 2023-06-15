using Invoice.Domain.Exceptions;
using Invoice.Domain.TaxRates;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests;
public class CreateTaxRateValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() => TaxRate.Create("10%", 10));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange            

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => TaxRate.Create("", 10));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => TaxRate.Create(null, 10));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => TaxRate.Create("10%__________________", 10));
    }

    [Fact]
    public void Value_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => TaxRate.Create("10%", -50));
    }    

    [Fact]
    public void Value_Greather_Than_100_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => TaxRate.Create("10%", 101));
    }    

}