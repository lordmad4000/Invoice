using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Api.Integration.Tests.InvoicesTests;

public class InvoicesFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public InvoicesFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact (Skip = "Preeliminary test")]
    public async Task GetAll_Then_GetFirst_ById()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);

        //Act
        var response = await client.GetAsync($"/api/Invoices/GetAll");
        var content = await response.Content.ReadAsStringAsync();
        var invoices = JsonConvert.DeserializeObject<List<InvoiceDto>>(content);
        if (invoices is null)
            invoices = new List<InvoiceDto>();

        //Assert
        Assert.True(invoices.Any(), "Not Invoices found. Add a Invoice and run again.");

        //Act
        response = await client.GetAsync($"/api/Invoices/GetById{invoices.First().Id}");
        content = await response.Content.ReadAsStringAsync();
        var invoice = JsonConvert.DeserializeObject<InvoiceDto>(content);
        if (invoice is null)
            invoice = new InvoiceDto();

        //Assert
        Assert.NotNull(invoice);
        Assert.Equal(invoices.First().Id, invoice.Id);
    }

    private InvoiceRegisterRequest GetInvoiceRegisterRequest() =>
        new InvoiceRegisterRequest
        {
            Number = "1",
            Description = "Test Invoice",
            CompanyName = "Company Test",
            CompanyIdDocumentType = "CIF",
            CompanyDocumentNumber = "A37610482",
            CompanyStreet = "Company Street Test",
            CompanyCity = "Company City Test",
            CompanyState = "Company State Test",
            CompanyCountry = "Company Country Test",
            CompanyPostalCode = "Company PostalCode Test",
            CompanyPhone = "+01 718 222 2222",
            CompanyEmail = "companytestmail@test.com",
            CustomerFullName = "Customer Test",
            CustomerIdDocumentType = "NIF",
            CustomerDocumentNumber = "36184297R",
            CustomerStreet = "Customer Street Test",
            CustomerCity = "Customer City Test",
            CustomerState = "Customer State Test",
            CustomerCountry = "Customer Country Test",
            CustomerPostalCode = "Customer PostalCode Test",
            CustomerPhone = "+01 718 222 2222",
            CustomerEmail = "customertestmail@test.com",
            InvoiceLines = GetInvoiceLinesRegisterRequest()
        };

    private List<InvoiceLineRegisterRequest> GetInvoiceLinesRegisterRequest() =>
        new List<InvoiceLineRegisterRequest>()
        {
            new InvoiceLineRegisterRequest
            {
                ProductCode = "LB",
                ProductName = "LASAÑA BOLOÑESA",
                ProductDescription = "LASAÑA BOLOÑESA",
                Packages = 1,
                Quantity = 1,
                Price = 2.60,
                Currency = "EUR",
                TaxName = "10%",
                TaxRate = 10,
                DiscountRate = 0
            },
            new InvoiceLineRegisterRequest
            {
                ProductCode = "BNN",
                ProductName = "BANANA",
                ProductDescription = "BANANA",
                Packages = 1,
                Quantity = 1.628,
                Price = 1.25,
                Currency = "EUR",
                TaxName = "0%",
                TaxRate = 0,
                DiscountRate = 0
            },
            new InvoiceLineRegisterRequest
            {
                ProductCode = "VFU",
                ProductName = "VAJILLAS FAIRY ULTRA",
                ProductDescription = "VAJILLAS FAIRY ULTRA",
                Packages = 2,
                Quantity = 2,
                Price = 3.25,
                Currency = "EUR",
                TaxName = "21%",
                TaxRate = 21,
                DiscountRate = 0
            },
            new InvoiceLineRegisterRequest
            {
                ProductCode = "PST",
                ProductName = "PIPA GIRASOL TOSTADA",
                ProductDescription = "PIPA GIRASOL TOSTADA",
                Packages = 3,
                Quantity = 3,
                Price = 0.95,
                Currency = "EUR",
                TaxName = "4%",
                TaxRate = 4,
                DiscountRate = 0
            },
        };
}