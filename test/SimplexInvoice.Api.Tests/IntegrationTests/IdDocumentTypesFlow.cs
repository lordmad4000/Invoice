using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class IdDocumentTypesFlow
{
    [Fact]
#pragma warning disable CS8602
    public async Task Register_IdDocumentType_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var idDocumentTypeDto = new IdDocumentTypeDto { Name = "TEST" };
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var idDocumentTypesController = serviceProvider.GetRequiredService<IdDocumentTypesController>();
            var idDocumentTypeRegisterRequest = new IdDocumentTypeRegisterRequest { Name = "TEST" };

            //Act
            IActionResult actionResult = await idDocumentTypesController.Register(idDocumentTypeRegisterRequest, new CancellationToken());

            //Assert
            var createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            idDocumentTypeDto = createdResult?.Value as IdDocumentTypeDto;
            Assert.NotNull(idDocumentTypeDto);
        };

        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var idDocumentTypesController = serviceProvider.GetRequiredService<IdDocumentTypesController>();
            idDocumentTypeDto.Name = "TEST MOD";

            //Act
            IActionResult actionResult = await idDocumentTypesController.Update(idDocumentTypeDto, new CancellationToken());

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            idDocumentTypeDto = okResult?.Value as IdDocumentTypeDto;
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.Delete(idDocumentTypeDto?.Id ?? Guid.Empty, new CancellationToken());

            //Assert
            okResult = actionResult as OkObjectResult;
            bool result = Assert.IsType<bool>(okResult.Value);
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Register_IdDocumentType_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var idDocumentTypeDto = new IdDocumentTypeDto { Name = "TEST" };
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var idDocumentTypesController = serviceProvider.GetRequiredService<IdDocumentTypesController>();
            var idDocumentTypeRegisterRequest = new IdDocumentTypeRegisterRequest { Name = "TEST" };

            //Act
            IActionResult actionResult = await idDocumentTypesController.Register(idDocumentTypeRegisterRequest, new CancellationToken());

            //Assert
            var createdResult = actionResult as CreatedResult;
            Assert.NotNull(createdResult);

            idDocumentTypeDto = createdResult?.Value as IdDocumentTypeDto;
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.GetById(idDocumentTypeDto.Id, new CancellationToken());

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            idDocumentTypeDto = okResult?.Value as IdDocumentTypeDto;
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.Delete(idDocumentTypeDto?.Id ?? Guid.Empty, new CancellationToken());

            //Assert
            okResult = actionResult as OkObjectResult;
            bool result = Assert.IsType<bool>(okResult.Value);
            Assert.True(result);
        };
    }

#pragma warning restore CS8602
}