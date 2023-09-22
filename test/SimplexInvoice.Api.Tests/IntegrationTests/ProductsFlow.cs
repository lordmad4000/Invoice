using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class ProductsFlow
{
    private Guid TaxRateId { get; set; } = Guid.Empty;
    public ProductsFlow()
    {
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {

            var taxRatesController = serviceProvider.GetRequiredService<TaxRatesController>();
            var actionResult = taxRatesController.GetAll(new CancellationToken());
            var taxRates = CustomConvert.OkResultTo<List<TaxRateDto>>(actionResult.Result);
            if (taxRates is not null)
                TaxRateId = taxRates.First().Id;
        }
    }

    [Fact]
    public async Task Register_Product_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        Guid productId = Guid.NewGuid();
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var productsController = serviceProvider.GetRequiredService<ProductsController>();
            ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();

            //Act
            IActionResult actionResult = await productsController.Register(productRegisterRequest, new CancellationToken());
            var productDto = CustomConvert.CreatedResultTo<ProductDto>(actionResult);

            //Assert
            Assert.NotNull(productDto);
            if (productDto is not null)
                productId = productDto.Id;
        };

        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var productsController = serviceProvider.GetRequiredService<ProductsController>();
            ProductDto? productDto = GetProductDto(productId);

            //Act
            IActionResult actionResult = await productsController.Update(productDto, new CancellationToken());
            productDto = CustomConvert.OkResultTo<ProductDto>(actionResult);

            //Assert
            Assert.NotNull(productDto);

            //Act
            actionResult = await productsController.Delete(productDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Register_Product_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var productsController = serviceProvider.GetRequiredService<ProductsController>();
            ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();

            //Act
            IActionResult actionResult = await productsController.Register(productRegisterRequest, new CancellationToken());
            var productDto = CustomConvert.CreatedResultTo<ProductDto>(actionResult);

            //Assert
            Assert.NotNull(productDto);
            if (productDto is null)
                throw new Exception("ProductDto is null.");

            //Act
            actionResult = await productsController.GetById(productDto.Id, new CancellationToken());
            productDto = CustomConvert.OkResultTo<ProductDto>(actionResult);

            //Assert
            Assert.NotNull(productDto);

            //Act
            actionResult = await productsController.Delete(productDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_Product_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var productsController = serviceProvider.GetRequiredService<ProductsController>();
            ProductRegisterRequest productRegisterRequest = GetProductRegisterRequest();

            //Act
            IActionResult actionResult = await productsController.Register(productRegisterRequest, new CancellationToken());
            var productDto = CustomConvert.CreatedResultTo<ProductDto>(actionResult);

            //Assert
            Assert.NotNull(productDto);

            //Act
            actionResult = await productsController.GetAll(new CancellationToken());
            var productDtos = CustomConvert.OkResultTo<List<ProductDto>>(actionResult);

            //Assert
            Assert.NotEmpty(productDtos);

            //Act
            actionResult = await productsController.Delete(productDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
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
            ProductTaxRateId = TaxRateId
        };

    private ProductDto GetProductDto(Guid id) =>
        new ProductDto
        {
            Id = id,
            Code = "CODE TEST MOD",
            Name = "NAME TEST MOD",
            Description = "DESCRIPTION TEST MOD",
            PackageQuantity = 1,
            Price = 1,
            Currency = "TES",
            ProductTaxRateId = TaxRateId
        };
}