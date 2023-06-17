using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Exceptions;
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
                           "24, White Dog St.", "Kingston", "New York", "12401",
                           "+01 718 222 2222",
                           "prueba@gmail.com"));

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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
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
                       "24, White Dog St.", "Kingston", "New York", "12401",
                       "+01 718 222 2222",
                       "prueba@gmail.com"));
    }

    [Fact]
    public void Empty_Phone_Should_Be_Throw_NotValidPhoneNumberException()
    {
        // Arrange            

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482                                ",
                           "24, White Dog St.", "Kingston", "New York", "12401",
                           "",
                           "prueba@gmail.com"));
    }

    [Fact]
    public void Null_Phone_Should_Be_Throw_NotValidPhoneNumberException()
    {
        // Arrange

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            Company.Create("Prueba S.L.",
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

        //Act & Assert
        Assert.Throws<NotValidPhoneNumberException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482                                ",
                           "24, White Dog St.", "Kingston", "New York", "12401",
                           "+01 718 222 2222 22245 64 8454 66 78 55 6",
                           "prueba@gmail.com"));
    }

    [Fact]
    public void Empty_Email_Should_Be_Throw_NotValidEmailAddressException()
    {
        // Arrange            

        //Act & Assert
        Assert.Throws<NotValidEmailAddressException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482                                ",
                           "24, White Dog St.", "Kingston", "New York", "12401",
                           "+01 718 222 2222",
                           ""));
    }

    [Fact]
    public void Null_Email_Should_Be_Throw_NotValidEmailAddressException()
    {
        // Arrange

        //Act & Assert
        Assert.Throws<NotValidEmailAddressException>(() =>
            Company.Create("Prueba S.L.",
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

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            Company.Create("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482                                ",
                           "24, White Dog St.", "Kingston", "New York", "12401",
                           "+01 718 222 2222",
                           "pruebaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"));
    }

}
