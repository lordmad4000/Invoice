using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.ValueObjects;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class CreateProductValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() =>
            Product.Create("10LB", 
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Code_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Code_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create(null,
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Code_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB_________________",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           null,
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Name_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA_________________________",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Empty_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           "",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           null,
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void Description_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA_________________________",
                           1.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void PackageQuantity_Equals_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           0.0,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void PackageQuantity_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           -0.5,
                           new Money("EUR", 2.60),
                           Guid.NewGuid()));
    }

    [Fact]
    public void UnitPriceAmount_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Product.Create("10LB",
                           "LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           1.0,
                           new Money("EUR", -1.50),
                           Guid.NewGuid()));
    }

}
