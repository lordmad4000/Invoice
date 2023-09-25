using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SimplexInvoice.Api.Controllers;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Api.Tests.IntegrationTests.Common;
using SimplexInvoice.SimplexInvoice.Api;

namespace SimplexInvoice.Api.Tests.IntegrationTests;

public class UsersFlow
{
    private Guid UserId { get; set; } = Guid.Empty;

    [Fact]
    public async Task Register_User_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var usersController = serviceProvider.GetRequiredService<UsersController>();
            UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

            //Act
            IActionResult actionResult = await usersController.Register(userRegisterRequest, new CancellationToken());
            var userResponse = CustomConvert.CreatedResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);
            if (userResponse is not null)
                UserId = userResponse.Id;
        };

        //Arrange
        services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var usersController = serviceProvider.GetRequiredService<UsersController>();
            UserUpdateRequest userUpdateRequest = GetUserUpdateRequest();

            //Act
            IActionResult actionResult = await usersController.Update(userUpdateRequest, new CancellationToken());
            var userResponse = CustomConvert.OkResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);

            //Act
            actionResult = await usersController.Delete(UserId, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Register_User_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var usersController = serviceProvider.GetRequiredService<UsersController>();
            UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

            //Act
            IActionResult actionResult = await usersController.Register(userRegisterRequest, new CancellationToken());
            var userResponse = CustomConvert.CreatedResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);
            if (userResponse is not null)
                UserId = userResponse.Id;

            //Act
            actionResult = await usersController.GetById(UserId, new CancellationToken());
            userResponse = CustomConvert.OkResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);

            //Act
            actionResult = await usersController.Delete(UserId, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_User_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var usersController = serviceProvider.GetRequiredService<UsersController>();
            UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

            //Act
            IActionResult actionResult = await usersController.Register(userRegisterRequest, new CancellationToken());
            var userResponse = CustomConvert.CreatedResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);
            if (userResponse is not null)
                UserId = userResponse.Id;

            //Act
            actionResult = await usersController.GetAll(new CancellationToken());
            var usersResponse = CustomConvert.OkResultTo<List<UserResponse>>(actionResult);

            //Assert
            Assert.NotEmpty(usersResponse);

            //Act
            actionResult = await usersController.Delete(UserId, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    [Fact]
    public async Task Register_User_Then_GetLast_1_Then_RemoveIt()
    {
        //Arrange
        IServiceCollection services = ServicesConfiguration.BuildDependencies();
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var usersController = serviceProvider.GetRequiredService<UsersController>();
            UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

            //Act
            IActionResult actionResult = await usersController.Register(userRegisterRequest, new CancellationToken());
            var userResponse = CustomConvert.CreatedResultTo<UserResponse>(actionResult);

            //Assert
            Assert.NotNull(userResponse);
            if (userResponse is not null)
                UserId = userResponse.Id;

            //Act
            actionResult = await usersController.GetLast(1, new CancellationToken());
            var usersResponse = CustomConvert.OkResultTo<List<UserResponse>>(actionResult);

            //Assert
            Assert.Single(usersResponse);

            //Act
            actionResult = await usersController.Delete(UserId, new CancellationToken());
            bool result = CustomConvert.OkResultTo<bool>(actionResult);

            //Assert
            Assert.True(result);
        };
    }

    private UserRegisterRequest GetUserRegisterRequest() =>
        new UserRegisterRequest
        {
            Email = "test@test.com",
            Password = "testpass",
            FirstName = "TEST FIRSTNAME",
            LastName = "TEST LASTNAME",
        };

    private UserUpdateRequest GetUserUpdateRequest() =>
        new UserUpdateRequest
        {
            Id = UserId,
            Email = "testmod@testmod.com",
            Password = "testmodpas",
            FirstName = "TEST FIRSTNAME MOD",
            LastName = "TEST LASTNAME MOD",
        };
}