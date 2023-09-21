using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class CustomersFlow
{
#pragma warning disable CS8602
    [Fact]
    public async Task Register_Customer_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        Guid customerId = Guid.NewGuid();
        Guid idDocumentTypeId = Guid.NewGuid();
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            idDocumentTypeId = await GetFirstIdDocumentTypeId(serviceProvider);
            var customersController = serviceProvider.GetRequiredService<CustomersController>();
            CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest(idDocumentTypeId);

            //Act
            IActionResult actionResult = await customersController.Register(customerRegisterRequest, new CancellationToken());
            var customerDto = CustomConvert.CreatedResultTo<CustomerDto>(actionResult);

            //Assert
            Assert.NotNull(customerDto);
            customerId = customerDto.Id;
        };

        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var customersController = serviceProvider.GetRequiredService<CustomersController>();
            CustomerUpdateRequest customerUpdateRequest = GetCustomerUpdateRequest(customerId, idDocumentTypeId);

            //Act
            IActionResult actionResult = await customersController.Update(customerUpdateRequest, new CancellationToken());
            var customerDto = CustomConvert.OkResultTo<CustomerDto>(actionResult);

            //Assert
            Assert.NotNull(customerDto);

            //Act
            actionResult = await customersController.Delete(customerDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Register_Customer_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            Guid idDocumentTypeId = await GetFirstIdDocumentTypeId(serviceProvider);
            var customersController = serviceProvider.GetRequiredService<CustomersController>();
            CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest(idDocumentTypeId);

            //Act
            IActionResult actionResult = await customersController.Register(customerRegisterRequest, new CancellationToken());
            var customerDto = CustomConvert.CreatedResultTo<CustomerDto>(actionResult);

            //Assert
            Assert.NotNull(customerDto);

            //Act
            actionResult = await customersController.GetById(customerDto.Id, new CancellationToken());
            customerDto = CustomConvert.OkResultTo<CustomerDto>(actionResult);

            //Assert
            Assert.NotNull(customerDto);

            //Act
            actionResult = await customersController.Delete(customerDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_Customer_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            Guid idDocumentTypeId = await GetFirstIdDocumentTypeId(serviceProvider);
            var customersController = serviceProvider.GetRequiredService<CustomersController>();
            CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest(idDocumentTypeId);

            //Act
            IActionResult actionResult = await customersController.Register(customerRegisterRequest, new CancellationToken());
            var customerDto = CustomConvert.CreatedResultTo<CustomerDto>(actionResult);

            //Assert
            Assert.NotNull(customerDto);

            //Act
            actionResult = await customersController.GetAll(new CancellationToken());
            var customerDtos = CustomConvert.OkResultTo<List<CustomerDto>>(actionResult);

            //Assert
            Assert.NotEmpty(customerDtos);

            //Act
            actionResult = await customersController.Delete(customerDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    private async Task<Guid> GetFirstIdDocumentTypeId(ServiceProvider serviceProvider)
    {
        var taxRatesController = serviceProvider.GetRequiredService<IdDocumentTypesController>();
        var actionResult = await taxRatesController.GetAll(new CancellationToken());
        var idDocumentTypes = CustomConvert.OkResultTo<List<IdDocumentTypeDto>>(actionResult);
        if (idDocumentTypes is null)
            throw new Exception("IdDocumentTypes not found.");

        return idDocumentTypes.First().Id;
    }

#pragma warning restore CS8602

    private CustomerDto GetCustomerDto()
    {
        CustomerDto customerDto = new CustomerDto
        {
            Id = Guid.Empty,
            FirstName = "TEST FIRSTNAME",
            LastName = "TEST LASTNAME",
            IdDocumentTypeId = Guid.NewGuid(),
            IdDocumentNumber = "TEST DOCUMENT NUMBER",
            Street = "TEST STREET",
            City = "TEST CITY",
            State = "TEST STATE",
            Country = "TEST COUNTRY",
            PostalCode = "TEST POSTAL CODE",
            Phone = "+01 718 222 2222",
            Email = "test@test.com"
        };

        return customerDto;
    }

    private CustomerRegisterRequest GetCustomerRegisterRequest(Guid idDocumentTypeId)
    {
        CustomerRegisterRequest customerDto = new CustomerRegisterRequest
        {
            FirstName = "TEST FIRSTNAME",
            LastName = "TEST LASTNAME",
            IdDocumentTypeId = idDocumentTypeId,
            IdDocumentNumber = "TEST DOCUMENT NUMBER",
            Street = "TEST STREET",
            City = "TEST CITY",
            State = "TEST STATE",
            Country = "TEST COUNTRY",
            PostalCode = "TEST POSTAL CODE",
            Phone = "+01 718 222 2222",
            Email = "test@test.com"
        };

        return customerDto;
    }

    private CustomerUpdateRequest GetCustomerUpdateRequest(Guid id, Guid idDocumentTypeId)
    {
        CustomerUpdateRequest customerDto = new CustomerUpdateRequest
        {
            Id = id,
            FirstName = "TEST FIRSTNAME MOD",
            LastName = "TEST LASTNAME MOD",
            IdDocumentTypeId = idDocumentTypeId,
            IdDocumentNumber = "TEST DOCUMENT NUMBER MOD",
            Street = "TEST STREET MOD",
            City = "TEST CITY MOD",
            State = "TEST STATE MOD",
            Country = "TEST COUNTRY MOD",
            PostalCode = "TEST POSTAL CODE MOD",
            Phone = "+01 718 222 2222",
            Email = "testmod@testmod.com"
        };

        return customerDto;
    }    


}