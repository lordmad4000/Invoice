using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.CustomersTests;

public class CustomersFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private Guid IdDocumentTypeId { get; set; } = Guid.Empty;

    public CustomersFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        var response = client.GetAsync("/api/IdDocumentTypes/GetAll");
        var content = response.Result.Content.ReadAsStringAsync();
        var idDocumentTypes = JsonConvert.DeserializeObject<List<IdDocumentTypeDto>>(content.Result);
        if (idDocumentTypes is not null)
            IdDocumentTypeId = idDocumentTypes.First().Id;
    }

    [Fact]
    public async Task Register_Customer_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest();
        CustomerUpdateRequest customerUpdateRequest = GetCustomerUpdateRequest();

        //Act
        var response = await client.PostAsync("/api/Customers/Register", JsonContent.Create(customerRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDto>(content);
        if (customer is null)
            customer = new CustomerDto();

        //Assert
        Assert.Equal(GetCustomerRegisterRequest().FirstName, customer.FirstName);
        Assert.Equal(GetCustomerRegisterRequest().LastName, customer.LastName);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentTypeId, customer.IdDocumentTypeId);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentNumber, customer.IdDocumentNumber);
        Assert.Equal(GetCustomerRegisterRequest().Street, customer.Street);
        Assert.Equal(GetCustomerRegisterRequest().City, customer.City);
        Assert.Equal(GetCustomerRegisterRequest().State, customer.State);
        Assert.Equal(GetCustomerRegisterRequest().Country, customer.Country);
        Assert.Equal(GetCustomerRegisterRequest().PostalCode, customer.PostalCode);
        Assert.Equal(GetCustomerRegisterRequest().Phone, customer.Phone);
        Assert.Equal(GetCustomerRegisterRequest().Email, customer.Email);

        //Act
        customerUpdateRequest.Id = customer.Id;
        response = await client.PutAsync("/api/Customers/Update", JsonContent.Create(customerUpdateRequest));
        content = await response.Content.ReadAsStringAsync();
        customer = JsonConvert.DeserializeObject<CustomerDto>(content);
        if (customer is null)
            customer = new CustomerDto();

        //Assert
        Assert.Equal(GetCustomerUpdateRequest().FirstName, customer.FirstName);
        Assert.Equal(GetCustomerUpdateRequest().LastName, customer.LastName);
        Assert.Equal(GetCustomerUpdateRequest().IdDocumentTypeId, customer.IdDocumentTypeId);
        Assert.Equal(GetCustomerUpdateRequest().IdDocumentNumber, customer.IdDocumentNumber);
        Assert.Equal(GetCustomerUpdateRequest().Street, customer.Street);
        Assert.Equal(GetCustomerUpdateRequest().City, customer.City);
        Assert.Equal(GetCustomerUpdateRequest().State, customer.State);
        Assert.Equal(GetCustomerUpdateRequest().Country, customer.Country);
        Assert.Equal(GetCustomerUpdateRequest().PostalCode, customer.PostalCode);
        Assert.Equal(GetCustomerUpdateRequest().Phone, customer.Phone);
        Assert.Equal(GetCustomerUpdateRequest().Email, customer.Email);

        //Act
        response = await client.DeleteAsync($"/api/Customers/Delete/{customer.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_Customer_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Customers/Register", JsonContent.Create(customerRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDto>(content);
        if (customer is null)
            customer = new CustomerDto();

        //Assert
        Assert.Equal(GetCustomerRegisterRequest().FirstName, customer.FirstName);
        Assert.Equal(GetCustomerRegisterRequest().LastName, customer.LastName);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentTypeId, customer.IdDocumentTypeId);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentNumber, customer.IdDocumentNumber);
        Assert.Equal(GetCustomerRegisterRequest().Street, customer.Street);
        Assert.Equal(GetCustomerRegisterRequest().City, customer.City);
        Assert.Equal(GetCustomerRegisterRequest().State, customer.State);
        Assert.Equal(GetCustomerRegisterRequest().Country, customer.Country);
        Assert.Equal(GetCustomerRegisterRequest().PostalCode, customer.PostalCode);
        Assert.Equal(GetCustomerRegisterRequest().Phone, customer.Phone);
        Assert.Equal(GetCustomerRegisterRequest().Email, customer.Email);

        //Act
        response = await client.GetAsync($"/api/Customers/GetById{customer.Id}");
        content = await response.Content.ReadAsStringAsync();
        customer = JsonConvert.DeserializeObject<CustomerDto>(content);
        if (customer is null)
            customer = new CustomerDto();

        //Assert
        Assert.Equal(GetCustomerRegisterRequest().FirstName, customer.FirstName);
        Assert.Equal(GetCustomerRegisterRequest().LastName, customer.LastName);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentTypeId, customer.IdDocumentTypeId);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentNumber, customer.IdDocumentNumber);
        Assert.Equal(GetCustomerRegisterRequest().Street, customer.Street);
        Assert.Equal(GetCustomerRegisterRequest().City, customer.City);
        Assert.Equal(GetCustomerRegisterRequest().State, customer.State);
        Assert.Equal(GetCustomerRegisterRequest().Country, customer.Country);
        Assert.Equal(GetCustomerRegisterRequest().PostalCode, customer.PostalCode);
        Assert.Equal(GetCustomerRegisterRequest().Phone, customer.Phone);
        Assert.Equal(GetCustomerRegisterRequest().Email, customer.Email);

        //Act
        response = await client.DeleteAsync($"/api/Customers/Delete/{customer.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_Customer_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        CustomerRegisterRequest customerRegisterRequest = GetCustomerRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Customers/Register", JsonContent.Create(customerRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDto>(content);
        if (customer is null)
            customer = new CustomerDto();

        //Assert
        Assert.Equal(GetCustomerRegisterRequest().FirstName, customer.FirstName);
        Assert.Equal(GetCustomerRegisterRequest().LastName, customer.LastName);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentTypeId, customer.IdDocumentTypeId);
        Assert.Equal(GetCustomerRegisterRequest().IdDocumentNumber, customer.IdDocumentNumber);
        Assert.Equal(GetCustomerRegisterRequest().Street, customer.Street);
        Assert.Equal(GetCustomerRegisterRequest().City, customer.City);
        Assert.Equal(GetCustomerRegisterRequest().State, customer.State);
        Assert.Equal(GetCustomerRegisterRequest().Country, customer.Country);
        Assert.Equal(GetCustomerRegisterRequest().PostalCode, customer.PostalCode);
        Assert.Equal(GetCustomerRegisterRequest().Phone, customer.Phone);
        Assert.Equal(GetCustomerRegisterRequest().Email, customer.Email);

        //Act
        response = await client.GetAsync($"/api/Customers/GetAll");
        content = await response.Content.ReadAsStringAsync();
        var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(content);
        if (customers is null)
            customers = new List<CustomerDto>();

        //Assert
        Assert.NotEmpty(customers);

        //Act
        response = await client.DeleteAsync($"/api/Customers/Delete/{customer.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    private CustomerRegisterRequest GetCustomerRegisterRequest() =>
        new CustomerRegisterRequest
        {
            FirstName = "TEST FIRSTNAME",
            LastName = "TEST LASTNAME",
            IdDocumentTypeId = IdDocumentTypeId,
            IdDocumentNumber = "TEST DOCUMENT NUMBER",
            Street = "TEST STREET",
            City = "TEST CITY",
            State = "TEST STATE",
            Country = "TEST COUNTRY",
            PostalCode = "TEST POSTAL CODE",
            Phone = "+01 718 222 2222",
            Email = "test@test.com"
        };

    private CustomerUpdateRequest GetCustomerUpdateRequest() =>
        new CustomerUpdateRequest
        {
            FirstName = "TEST FIRSTNAME MOD",
            LastName = "TEST LASTNAME MOD",
            IdDocumentTypeId = IdDocumentTypeId,
            IdDocumentNumber = "TEST DOCUMENT NUMBER MOD",
            Street = "TEST STREET MOD",
            City = "TEST CITY MOD",
            State = "TEST STATE MOD",
            Country = "TEST COUNTRY MOD",
            PostalCode = "TEST POSTAL CODE MOD",
            Phone = "+01 718 222 2222",
            Email = "testmod@testmod.com"
        };
}