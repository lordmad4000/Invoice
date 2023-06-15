using Invoice.Domain.Exceptions;
using Invoice.Domain.Products;
using System;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests;
public class CreateProductValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() =>
            Product.Create("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create(null,
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Name_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("LASAÑA BOLOGNESA_________________________",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Empty_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("LASAÑA BOLOGNESA",
                           "",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("LASAÑA BOLOGNESA",
                           null,
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Description_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA_________________________",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void UnitPrice_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           -1.50,
                           Guid.NewGuid()));
    }

}
