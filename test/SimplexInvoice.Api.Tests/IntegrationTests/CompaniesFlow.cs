using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class CompaniesFlow
{
    private CompanyDto Company { get; set; } = new CompanyDto();
    public CompaniesFlow()
    {
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            var actionResult = companysController.GetAll(new CancellationToken());
            var companyDtos = CustomConvert.OkResultTo<List<CompanyDto>>(actionResult.Result);
            if (companyDtos is not null)
                Company = companyDtos.First();
        };
    }

    [Fact]
    public async Task ModifyNameProperty_Then_RestoreNameProperty()
    {
        //Arrange
        string oldCompanyName = Company.Name;
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            CompanyRegisterUpdateRequest companyRegisterUpdateRequest = GetCompanyRegisterUpdateRequest();

            //Act
            IActionResult actionResult = await companysController.RegisterUpdate(companyRegisterUpdateRequest, new CancellationToken());
            var companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult);

            //Assert
            Assert.NotNull(companyDto);
        }
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();
            CompanyRegisterUpdateRequest companyRegisterUpdateRequest = GetCompanyRegisterUpdateRequest();
            companyRegisterUpdateRequest.Name = oldCompanyName;

            //Act
            IActionResult actionResult = await companysController.RegisterUpdate(companyRegisterUpdateRequest, new CancellationToken());
            var companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult);

            //Assert
            Assert.NotNull(companyDto);
        }

    }

    [Fact]
    public async Task GetById_Should_No_Be_Null()
    {
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();

            //Act
            IActionResult actionResult = await companysController.GetById(Company.Id, new CancellationToken());
            var companyDto = CustomConvert.OkResultTo<CompanyDto>(actionResult);

            //Assert
            Assert.NotNull(companyDto);
        }
    }

    [Fact]
    public async Task GetAll_Count_Should_Be_1()
    {
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var companysController = serviceProvider.GetRequiredService<CompaniesController>();

            //Act
            IActionResult actionResult = await companysController.GetAll(new CancellationToken());
            var companyDtos = CustomConvert.OkResultTo<List<CompanyDto>>(actionResult);

            //Assert
            Assert.Single(companyDtos);
        }
    }   

    private CompanyRegisterUpdateRequest GetCompanyRegisterUpdateRequest() =>
        new CompanyRegisterUpdateRequest
        {
            Name = "TEST NAME",
            IdDocumentTypeId = Company.IdDocumentTypeId,
            IdDocumentNumber = Company.IdDocumentNumber,
            Street = Company.Street,
            City = Company.City,
            State = Company.State,
            Country = Company.Country,
            PostalCode = Company.PostalCode,
            Phone = Company.Phone,
            Email = Company.Email
        };

}