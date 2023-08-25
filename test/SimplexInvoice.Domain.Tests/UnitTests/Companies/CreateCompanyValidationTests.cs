using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;

public class CreateCompanyValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act
        var exception = Record.Exception(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create(null,
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void Name_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba                               S.L.",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void Empty_IdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void Null_IdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           null,
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void IdDocumentNumber_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        // Arrange

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482                                ",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com")));
    }

    [Fact]
    public void Email_Length_Greater_Than_40_Should_Throw_BusinessRuleValidationException()
    {
        //Arrange

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("pruebaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com")));
    }

}
