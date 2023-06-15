using Invoice.Domain.Exceptions;
using Invoice.Domain.TaxRates;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests;
public class UpdateTaxRateValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act
        var exception = Record.Exception(() => taxRate.Update("21%", 21));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => taxRate.Update("", 10));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => taxRate.Update(null, 10));
    }

    [Fact]
    public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => taxRate.Update("10%__________________", 10));
    }

    [Fact]
    public void Value_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => taxRate.Update("10%", -50));
    }

    [Fact]
    public void Value_Greather_Than_100_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var taxRate = TaxRate.Create("10%", 10);

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() => taxRate.Update("10%", 101));
    }

    // [Fact]
    // public void Should_Not_Be_Throw_BusinessRuleValidationException()
    // {
    //     //Arrange
    //     var idDocumentType = IdDocumentType.Create("DNI");

    //     //Act
    //     var exception = Record.Exception(() => idDocumentType.Update("NIF"));

    //     //Assert
    //     Assert.Null(exception);
    // }

    // [Fact]
    // public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    // {
    //     // Arrange
    //     var idDocumentType = IdDocumentType.Create("DNI");

    //     //Act & Assert
    //     Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update(""));
    // }

    // [Fact]
    // public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    // {
    //     // Arrange
    //     var idDocumentType = IdDocumentType.Create("DNI");

    //     //Act & Assert
    //     Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update(null));
    // }

    // [Fact]
    // public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    // {
    //     //Arrange
    //     var idDocumentType = IdDocumentType.Create("DNI");

    //     //Act & Assert
    //     Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update("DNI__________________"));
    // }

}