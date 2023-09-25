using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class IdDocumentTypesFlow
{
    [Fact]
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
            idDocumentTypeDto = CustomConvert.CreatedResultTo<IdDocumentTypeDto>(actionResult);

            //Assert
            Assert.NotNull(idDocumentTypeDto);
        };

        //Arrange
        if (idDocumentTypeDto is null)
            throw new Exception("IdDocumentTypeDto is null.");

        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var idDocumentTypesController = serviceProvider.GetRequiredService<IdDocumentTypesController>();
            idDocumentTypeDto.Name = "TEST MOD";

            //Act
            IActionResult actionResult = await idDocumentTypesController.Update(idDocumentTypeDto, new CancellationToken());
            idDocumentTypeDto = CustomConvert.OkResultTo<IdDocumentTypeDto>(actionResult);

            //Assert
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.Delete(idDocumentTypeDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
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
            idDocumentTypeDto = CustomConvert.CreatedResultTo<IdDocumentTypeDto>(actionResult);

            //Assert
            Assert.NotNull(idDocumentTypeDto);
            if (idDocumentTypeDto is null)
                throw new Exception("IdDocumentTypeDto is null.");

            //Act
            actionResult = await idDocumentTypesController.GetById(idDocumentTypeDto.Id, new CancellationToken());
            idDocumentTypeDto = CustomConvert.OkResultTo<IdDocumentTypeDto>(actionResult);

            //Assert
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.Delete(idDocumentTypeDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_IdDocumentType_Then_GetAll_Then_RemoveIt()
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
            idDocumentTypeDto = CustomConvert.CreatedResultTo<IdDocumentTypeDto>(actionResult);

            //Assert
            Assert.NotNull(idDocumentTypeDto);

            //Act
            actionResult = await idDocumentTypesController.GetAll(new CancellationToken());
            var idDocumentTypeDtos = CustomConvert.OkResultTo<List<IdDocumentTypeDto>>(actionResult);

            //Assert
            Assert.NotEmpty(idDocumentTypeDtos);

            //Act
            actionResult = await idDocumentTypesController.Delete(idDocumentTypeDto?.Id ?? Guid.Empty, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

}