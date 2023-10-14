using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class CompaniesFlow
{

    [Fact]
    public async Task ModifyNameProperty_Then_RestoreNameProperty()
    {
        //Arrange
        CompanyDto? companyDto = new CompanyDto();
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            var actionResult = companysController.Get(new CancellationToken());
            companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult.Result);
            if (companyDto is null)
                throw new Exception("Error al recuperar la compañia, no se pueden ejecutar los tests.");
        };
        string oldCompanyName = companyDto.Name;
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            CompanyRegisterUpdateRequest companyRegisterUpdateRequest = GetCompanyRegisterUpdateRequest(companyDto);

            //Act
            IActionResult actionResult = await companysController.RegisterUpdate(companyRegisterUpdateRequest, new CancellationToken());
            companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult);

            //Assert
            Assert.NotNull(companyDto);
        }
        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            CompanyRegisterUpdateRequest companyRegisterUpdateRequest = GetCompanyRegisterUpdateRequest(companyDto);
            companyRegisterUpdateRequest.Name = oldCompanyName;

            //Act
            IActionResult actionResult = await companysController.RegisterUpdate(companyRegisterUpdateRequest, new CancellationToken());
            companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult);

            //Assert
            Assert.NotNull(companyDto);
        }

    }

    private CompanyRegisterUpdateRequest GetCompanyRegisterUpdateRequest(CompanyDto company) =>
        new CompanyRegisterUpdateRequest
        {
            Name = "TEST NAME",
            IdDocumentTypeId = company.IdDocumentTypeId,
            IdDocumentNumber = company.IdDocumentNumber,
            Street = company.Street,
            City = company.City,
            State = company.State,
            Country = company.Country,
            PostalCode = company.PostalCode,
            Phone = company.Phone,
            Email = company.Email
        };

}