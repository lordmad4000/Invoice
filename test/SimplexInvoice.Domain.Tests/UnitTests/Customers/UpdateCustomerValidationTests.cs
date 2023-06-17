using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.Exceptions;
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
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "+01 718 222 2222",
                            "prueba@gmail.com"));

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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
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
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com"));
    }

    [Fact]
    public void Empty_Phone_Should_Be_Throw_NotValidPhoneNumberException()
    {
        //Arrange            
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "",
                            "prueba@gmail.com"));
    }

    [Fact]
    public void Null_Phone_Should_Be_Throw_NotValidPhoneNumberException()
    {
        //Arrange
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            null,
                            "prueba@gmail.com"));
    }

    [Fact]
    public void Phone_Lengtg_Greater_Than_40_Should_Throw_NotValidPhoneNumberException()
    {
        //Arrange
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "+01 718 222 2222 22245 64 8454 66 78 55 6",
                            "prueba@gmail.com"));
    }

    [Fact]
    public void Empty_Email_Should_Be_Throw_NotValidEmailAddressException()
    {
        //Arrange            
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<NotValidEmailAddressException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "+01 718 222 2222",
                            ""));
    }

    [Fact]
    public void Null_Email_Should_Be_Throw_NotValidEmailAddressException()
    {
        //Arrange
        var customer = GetCustomer();

        //Act & Assert
        Assert.Throws<NotValidEmailAddressException>(() =>
            customer.Update("Kyle",
                            "Reese",
                            Guid.NewGuid(),
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "+01 718 222 2222",
                            null));
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
                            "A37610482                                ",
                            "24, White Dog St.", "Kingston", "New York", "12401",
                            "+01 718 222 2222",
                            "pruebaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"));
    }

    private static Customer GetCustomer() =>
        Customer.Create("John",
                        "Connor",
                        Guid.NewGuid(),
                        "A37610482",
                        "24, White Dog St.", "Kingston", "New York", "12401",
                        "+01 718 222 2222",
                        "prueba@gmail.com");

}
