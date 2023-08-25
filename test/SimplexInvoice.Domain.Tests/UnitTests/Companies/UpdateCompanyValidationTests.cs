using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;

public class UpdateCompanyValidationTests
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var company = GetCompany();

        //Act
        var exception = Record.Exception(() =>
            company.Update("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                           new PhoneNumber("+45 678 598 4712"),
                           new EmailAddress("example@hotmail.com")));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                           new PhoneNumber("+45 678 598 4712"),
                           new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update(null,
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                           new PhoneNumber("+45 678 598 4712"),
                           new EmailAddress("example@hotmail.com")));
    }

    [Fact]
    public void Name_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("Prueba                               S.L.",
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
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("Prueba S.L.",
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
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("Prueba S.L.",
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
        var company = GetCompany();

        // Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("Prueba S.L.",
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
        var company = GetCompany();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            company.Update("Prueba S.L.",
                           Guid.NewGuid(),
                           "A37610482",
                           new Address("31, Black Cat Av.", "Austin", "Texas", "USA", "58694"),
                           new PhoneNumber("+45 678 598 4712"),
                           new EmailAddress("exampleeeeeeeeeeeeeeeeeeeeeee@hotmail.com")));
    }

    private static Company GetCompany() =>
        Company.Create("Prueba S.L.",
                       Guid.NewGuid(),
                       "A37610482",
                       new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                       new PhoneNumber("+01 718 222 2222"),
                       new EmailAddress("prueba@gmail.com"));
}
