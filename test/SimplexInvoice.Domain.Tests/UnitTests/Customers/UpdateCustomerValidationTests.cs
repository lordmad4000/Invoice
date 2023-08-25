using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class UpdateCustomerValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        //Act
        var exception = Record.Exception(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_FirstName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Null_FirstName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update(null,
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void FirstName_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle_____________________________________",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Empty_LastName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Null_LastName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            null,
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void LastName_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "Reese____________________________________",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Empty_IdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Null_IdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            null,
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void IdDocumentNumber_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Email_Length_Greater_Than_40_Should_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482",
                            new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                            new PhoneNumber("+45 678 598 4712"),
                            new EmailAddress("exampleeeeeeeeeeeeeeeeeeeeeee@hotmail.com")));
    }

    private static Customer GetCustomer() =>
        Customer.Create("John",
                        "Connor",
                        Guid.NewGuid(),
                        "A37610482",
                        new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                        new PhoneNumber("+01 718 222 2222"),
                        new EmailAddress("prueba@gmail.com"));

}
