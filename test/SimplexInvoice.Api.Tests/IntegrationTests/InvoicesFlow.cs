using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class InvoicesFlow
{
    private Guid InvoiceId { get; set; } = Guid.Empty;

    [Fact]
    public async Task Register_Invoice_Then_GetById_Then_GetAll()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var invoicesController = serviceProvider.GetRequiredService<InvoicesController>();
            InvoiceRegisterRequest invoiceRegisterRequest = GetInvoiceRegisterRequest();

            //Act
            IActionResult actionResult = await invoicesController.Register(invoiceRegisterRequest, new CancellationToken());
            var invoiceDto = CustomConvert.CreatedResultTo<InvoiceDto>(actionResult);

            //Assert
            Assert.NotNull(invoiceDto);
            if (invoiceDto is not null)
                InvoiceId = invoiceDto.Id;

            //Act
            actionResult = await invoicesController.GetById(InvoiceId, new CancellationToken());
            invoiceDto = CustomConvert.OkResultTo<InvoiceDto>(actionResult);

            //Assert
            Assert.NotNull(invoiceDto);

            //Act
            actionResult = await invoicesController.GetAll(new CancellationToken());
            var invoiceDtos = CustomConvert.OkResultTo<List<InvoiceDto>>(actionResult);

            //Assert
            Assert.NotEmpty(invoiceDtos);
        };
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