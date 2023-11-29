using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.TaxRatesTests;

public class TaxRatesFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public TaxRatesFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Register_TaxRate_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        TaxRateRegisterRequest taxRateRegisterRequest = GetTaxRateRegisterRequest();
        TaxRateDto taxRateDto = GetTaxRateDto();

        //Act
        var response = await client.PostAsync("/api/TaxRates/Register", JsonContent.Create(taxRateRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var taxRate = JsonConvert.DeserializeObject<TaxRateDto>(content);
        if (taxRate is null)
            taxRate = new TaxRateDto();

        //Assert
        Assert.Equal(GetTaxRateRegisterRequest().Name, taxRate.Name);
        Assert.Equal(GetTaxRateRegisterRequest().Value, taxRate.Value);

        //Act
        taxRateDto.Id = taxRate.Id;
        response = await client.PutAsync("/api/TaxRates/Update", JsonContent.Create(taxRateDto));
        content = await response.Content.ReadAsStringAsync();
        taxRate = JsonConvert.DeserializeObject<TaxRateDto>(content);
        if (taxRate is null)
            taxRate = new TaxRateDto();

        //Assert
        Assert.Equal(GetTaxRateDto().Name, taxRate.Name);
        Assert.Equal(GetTaxRateDto().Value, taxRate.Value);

        //Act
        response = await client.DeleteAsync($"/api/TaxRates/Delete/{taxRate.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_TaxRate_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        TaxRateRegisterRequest taxRateRegisterRequest = GetTaxRateRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/TaxRates/Register", JsonContent.Create(taxRateRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var taxRate = JsonConvert.DeserializeObject<TaxRateDto>(content);
        if (taxRate is null)
            taxRate = new TaxRateDto();

        //Assert
        Assert.Equal(GetTaxRateRegisterRequest().Name, taxRate.Name);
        Assert.Equal(GetTaxRateRegisterRequest().Value, taxRate.Value);

        //Act
        response = await client.GetAsync($"/api/TaxRates/GetById{taxRate.Id}");
        content = await response.Content.ReadAsStringAsync();
        taxRate = JsonConvert.DeserializeObject<TaxRateDto>(content);
        if (taxRate is null)
            taxRate = new TaxRateDto();

        //Assert
        Assert.Equal(GetTaxRateRegisterRequest().Name, taxRate.Name);
        Assert.Equal(GetTaxRateRegisterRequest().Value, taxRate.Value);

        //Act
        response = await client.DeleteAsync($"/api/TaxRates/Delete/{taxRate.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_TaxRate_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        TaxRateRegisterRequest taxRateRegisterRequest = GetTaxRateRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/TaxRates/Register", JsonContent.Create(taxRateRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var taxRate = JsonConvert.DeserializeObject<TaxRateDto>(content);
        if (taxRate is null)
            taxRate = new TaxRateDto();

        //Assert
        Assert.Equal(GetTaxRateRegisterRequest().Name, taxRate.Name);
        Assert.Equal(GetTaxRateRegisterRequest().Value, taxRate.Value);

        //Act
        response = await client.GetAsync($"/api/TaxRates/GetAll");
        content = await response.Content.ReadAsStringAsync();
        var taxRates = JsonConvert.DeserializeObject<List<TaxRateDto>>(content);
        if (taxRates is null)
            taxRates = new List<TaxRateDto>();

        //Assert
        Assert.NotEmpty(taxRates);

        //Act
        response = await client.DeleteAsync($"/api/TaxRates/Delete/{taxRate.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    private TaxRateRegisterRequest GetTaxRateRegisterRequest() =>
        new TaxRateRegisterRequest
        {
            Name = "TEST",
            Value = 1
        };

    private TaxRateDto GetTaxRateDto() =>
        new TaxRateDto
        {
            Name = "TEST MOD",
            Value = 2
        };

}