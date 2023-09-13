using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.ValueObjects;
using System.Collections.Generic;
using System;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests;
public class UpdateInvoiceValidationTests : InvoiceSharedData
{
    [Fact]
    public void Should_Not_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act
        var exception = Record.Exception(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Empty_Number_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_Number_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update(null,
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_Description_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           null,
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Description_Length_Greather_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice_____________________________",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CompanyName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CompanyName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           null,
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CompanyName_Length_Greather_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test_____________________________",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CompanyIdDocumentType_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CompanyIdDocumentType_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           null,
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CompanyIdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CompanyIdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           null,
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CompanyIdDocumentNumber_Length_Greather_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482_________________________________",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CompanyEmailAddress_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("pruebaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CustomerFullName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CustomerFullName_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           null,
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CustomerFullName_Length_Greather_Than_81_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test_____________________________________________________________________",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CustomerIdDocumentType_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CustomerIdDocumentType_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           null,
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Empty_CustomerIdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void Null_CustomerIdDocumentNumber_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           null,
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CustomerIdDocumentNumber_Length_Greather_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R________________________________",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void CustomerEmailAddress_Length_Greater_Than_40_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("pruebaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"),
                           GetInvoiceLines()));
    }

    [Fact]
    public void InvoiceLines_Length_Lesser_Than_1_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange
        Invoice invoice = GetInvoice();

        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           new List<InvoiceLine>()));
    }

    [Fact]
    public void InvoiceLines_With_Distinct_Currencies_Should_Be_Throw_BusinessRuleValidationException()
    {
        //Arrange        
        var invoice =  Invoice.Create("1",
                                      "Test Invoice",
                                      "Company Test",
                                      "CIF",
                                      "A37610482",
                                      new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                                      new PhoneNumber("+01 718 222 2222"),
                                      new EmailAddress("prueba@gmail.com"),
                                      "Customer Test",
                                      "NIF",
                                      "36184297R",
                                      new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                                      new PhoneNumber("+01 718 222 2222"),
                                      new EmailAddress("prueba@gmail.com"),
                                      new List<InvoiceLine>
                                      {
                                           InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0),
                                      });


        //Act & Assert
        Assert.Throws<BusinessRuleValidationException>(() =>
            invoice.Update("1",
                           "Test Invoice",
                           "Company Test",
                           "CIF",
                           "A37610482",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           "Customer Test",
                           "NIF",
                           "36184297R",
                           new Address("24, White Dog St.", "Kingston", "New York", "USA", "12401"),
                           new PhoneNumber("+01 718 222 2222"),
                           new EmailAddress("prueba@gmail.com"),
                           new List<InvoiceLine>
                           {
                               InvoiceLine.Create(Guid.NewGuid(), 1, "BNN", "BANANA", "BANANA", 1, 0.785, new Money("USD", 1.25), "0%", 0, 0),
                               InvoiceLine.Create(Guid.NewGuid(), 2, "PST", "PIPA GIRASOL TOSTADA", "PIPA GIRASOL TOSTADA", 2, 2, new Money("EUR", 0.95), "4%", 4, 0)
                           }));
    }

}