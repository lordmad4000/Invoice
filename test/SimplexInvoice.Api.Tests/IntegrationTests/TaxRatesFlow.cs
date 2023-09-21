using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class TaxRatesFlow
{
#pragma warning disable CS8602
    [Fact]
    public async Task Register_TaxRate_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var taxRateDto = new TaxRateDto { Name = "TEST", Value = 1 };
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var taxRatesController = serviceProvider.GetRequiredService<TaxRatesController>();
            var taxRateRegisterRequest = new TaxRateRegisterRequest { Name = "TEST", Value = 1 };

            //Act
            IActionResult actionResult = await taxRatesController.Register(taxRateRegisterRequest, new CancellationToken());

            //Assert
            var createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            taxRateDto = createdResult?.Value as TaxRateDto;
            Assert.NotNull(taxRateDto);
        };

        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var taxRatesController = serviceProvider.GetRequiredService<TaxRatesController>();
            taxRateDto.Name = "TEST MOD";
            taxRateDto.Value = 2;

            //Act
            IActionResult actionResult = await taxRatesController.Update(taxRateDto, new CancellationToken());

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            taxRateDto = okResult?.Value as TaxRateDto;
            Assert.NotNull(taxRateDto);

            //Act
            actionResult = await taxRatesController.Delete(taxRateDto?.Id ?? Guid.Empty, new CancellationToken());

            //Assert
            okResult = actionResult as OkObjectResult;
            bool result = Assert.IsType<bool>(okResult.Value);
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Register_TaxRate_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var taxRateDto = new TaxRateDto { Name = "TEST" };
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var taxRatesController = serviceProvider.GetRequiredService<TaxRatesController>();
            var taxRateRegisterRequest = new TaxRateRegisterRequest { Name = "TEST" };

            //Act
            IActionResult actionResult = await taxRatesController.Register(taxRateRegisterRequest, new CancellationToken());

            //Assert
            var createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            taxRateDto = createdResult?.Value as TaxRateDto;
            Assert.NotNull(taxRateDto);

            //Act
            actionResult = await taxRatesController.GetById(taxRateDto.Id, new CancellationToken());

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            taxRateDto = okResult?.Value as TaxRateDto;
            Assert.NotNull(taxRateDto);

            //Act
            actionResult = await taxRatesController.Delete(taxRateDto?.Id ?? Guid.Empty, new CancellationToken());

            //Assert
            okResult = actionResult as OkObjectResult;
            bool result = Assert.IsType<bool>(okResult.Value);
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_TaxRate_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var taxRateDto = new TaxRateDto { Name = "TEST" };
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var taxRatesController = serviceProvider.GetRequiredService<TaxRatesController>();
            var taxRateRegisterRequest = new TaxRateRegisterRequest { Name = "TEST" };

            //Act
            IActionResult actionResult = await taxRatesController.Register(taxRateRegisterRequest, new CancellationToken());

            //Assert
            var createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            taxRateDto = createdResult?.Value as TaxRateDto;
            Assert.NotNull(taxRateDto);

            //Act
            actionResult = await taxRatesController.GetAll(new CancellationToken());

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var taxRateDtos = okResult?.Value as List<TaxRateDto>;
            Assert.NotEmpty(taxRateDtos);

            //Act
            actionResult = await taxRatesController.Delete(taxRateDto?.Id ?? Guid.Empty, new CancellationToken());

            //Assert
            okResult = actionResult as OkObjectResult;
            bool result = Assert.IsType<bool>(okResult.Value);
            Assert.True(result);
        };
    }
#pragma warning restore CS8602

}