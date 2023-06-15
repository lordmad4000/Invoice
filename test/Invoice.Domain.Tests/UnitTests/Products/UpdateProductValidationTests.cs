using Invoice.Domain.Exceptions;
using Invoice.Domain.Products;
using System;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests;
public class UpdateProductValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act
        var exception = Record.Exception(() =>
            product.Update("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update(null,
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Name_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("LASAÑA BOLOGNESA_________________________",
                           "LASAÑA BOLOGNESA",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Empty_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("LASAÑA BOLOGNESA",
                           "",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Null_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("LASAÑA BOLOGNESA",
                           null,
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void Description_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA_________________________",
                           2.60,
                           Guid.NewGuid()));
    }

    [Fact]
    public void UnitPrice_Lesser_Than_0_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var product = GetProduct();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            product.Update("LASAÑA BOLOGNESA",
                           "LASAÑA BOLOGNESA",
                           -1.50,
                           Guid.NewGuid()));
    }

    private static Product GetProduct() =>
        Product.Create("FAIRY ULTRA",
                       "FAIRY ULTRA",
                       3.25,
                       Guid.NewGuid());

}
