using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.ProductsTests;

public class ProductsFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private Guid TaxRateId { get; set; } = Guid.Empty;

    public ProductsFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        var response = client.GetAsync("/api/TaxRates/GetAll");
        var content = response.Result.Content.ReadAsStringAsync();
        var taxRates = JsonConvert.DeserializeObject<List<IdDocumentTypeDto>>(content.Result);
        if (taxRates is not null)
            TaxRateId = taxRates.First().Id;
    }

    [Fact]
    public async Task Register_Product_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();
        ProductDto productUpdateRequest = GetProductDto();

        //Act
        var response = await client.PostAsync("/api/Products/Register", JsonContent.Create(productRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductDto>(content);
        if (product is null)
            product = new ProductDto();

        //Assert
        Assert.Equal(GetProductRegisterRequest().Code, product.Code);
        Assert.Equal(GetProductRegisterRequest().Name, product.Name);
        Assert.Equal(GetProductRegisterRequest().Description, product.Description);
        Assert.Equal(GetProductRegisterRequest().PackageQuantity, product.PackageQuantity);
        Assert.Equal(GetProductRegisterRequest().Price, product.Price);
        Assert.Equal(GetProductRegisterRequest().Currency, product.Currency);
        Assert.Equal(GetProductRegisterRequest().TaxRateId, product.TaxRateId);

        //Arrange
        productUpdateRequest.Id = product.Id;

        //Act
        response = await client.PutAsync("/api/Products/Update", JsonContent.Create(productUpdateRequest));
        content = await response.Content.ReadAsStringAsync();
        product = JsonConvert.DeserializeObject<ProductDto>(content);
        if (product is null)
            product = new ProductDto();

        //Assert
        Assert.Equal(GetProductDto().Code, product.Code);
        Assert.Equal(GetProductDto().Name, product.Name);
        Assert.Equal(GetProductDto().Description, product.Description);
        Assert.Equal(GetProductDto().PackageQuantity, product.PackageQuantity);
        Assert.Equal(GetProductDto().Price, product.Price);
        Assert.Equal(GetProductDto().Currency, product.Currency);
        Assert.Equal(GetProductDto().TaxRateId, product.TaxRateId);

        //Act
        response = await client.DeleteAsync($"/api/Products/Delete/{product.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_Product_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Products/Register", JsonContent.Create(productRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductDto>(content);
        if (product is null)
            product = new ProductDto();

        //Assert
        Assert.Equal(GetProductRegisterRequest().Code, product.Code);
        Assert.Equal(GetProductRegisterRequest().Name, product.Name);
        Assert.Equal(GetProductRegisterRequest().Description, product.Description);
        Assert.Equal(GetProductRegisterRequest().PackageQuantity, product.PackageQuantity);
        Assert.Equal(GetProductRegisterRequest().Price, product.Price);
        Assert.Equal(GetProductRegisterRequest().Currency, product.Currency);
        Assert.Equal(GetProductRegisterRequest().TaxRateId, product.TaxRateId);

        //Act
        response = await client.GetAsync($"/api/Products/GetById{product.Id}");
        content = await response.Content.ReadAsStringAsync();
        product = JsonConvert.DeserializeObject<ProductDto>(content);
        if (product is null)
            product = new ProductDto();

        //Assert
        Assert.Equal(GetProductRegisterRequest().Code, product.Code);
        Assert.Equal(GetProductRegisterRequest().Name, product.Name);
        Assert.Equal(GetProductRegisterRequest().Description, product.Description);
        Assert.Equal(GetProductRegisterRequest().PackageQuantity, product.PackageQuantity);
        Assert.Equal(GetProductRegisterRequest().Price, product.Price);
        Assert.Equal(GetProductRegisterRequest().Currency, product.Currency);
        Assert.Equal(GetProductRegisterRequest().TaxRateId, product.TaxRateId);

        //Act
        response = await client.DeleteAsync($"/api/Products/Delete/{product.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_Product_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Products/Register", JsonContent.Create(productRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<ProductDto>(content);
        if (product is null)
            product = new ProductDto();

        //Assert
        Assert.Equal(GetProductRegisterRequest().Code, product.Code);
        Assert.Equal(GetProductRegisterRequest().Name, product.Name);
        Assert.Equal(GetProductRegisterRequest().Description, product.Description);
        Assert.Equal(GetProductRegisterRequest().PackageQuantity, product.PackageQuantity);
        Assert.Equal(GetProductRegisterRequest().Price, product.Price);
        Assert.Equal(GetProductRegisterRequest().Currency, product.Currency);
        Assert.Equal(GetProductRegisterRequest().TaxRateId, product.TaxRateId);

        //Act
        response = await client.GetAsync($"/api/Products/GetAll");
        content = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<ProductDto>>(content);
        if (products is null)
            products = new List<ProductDto>();

        //Assert
        Assert.NotEmpty(products);

        //Act
        response = await client.DeleteAsync($"/api/Products/Delete/{product.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    private ProductRegisterRequest GetProductRegisterRequest() =>
        new ProductRegisterRequest
        {
            Code = "CODE TEST",
            Name = "NAME TEST",
            Description = "DESCRIPTION TEST",
            PackageQuantity = 1,
            Price = 1,
            Currency = "TES",
            TaxRateId = TaxRateId
        };

    private ProductDto GetProductDto() =>
    new ProductDto
    {
        Code = "CODE TEST MOD",
        Name = "NAME TEST MOD",
        Description = "DESCRIPTION TEST MOD",
        PackageQuantity = 1,
        Price = 1,
        Currency = "TES",
        TaxRateId = TaxRateId
    };

}