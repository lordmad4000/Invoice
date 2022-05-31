// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.DependencyInjection;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using System;
// using TestUsers.API.Mock;
// using Users.API.Controllers;
// using Users.Application.Interfaces;
// using Users.Application.Models;
// using Users.Application.Services;
// using Users.Domain.Interfaces;
// using Xunit;

// namespace TestUsers.API
// {
//     public class UserControllerTest
//     {

//         [Fact]
//         public async Task PostUser_Then_GetUsers_Then_GetUser__Then_UpdateUser_Then_DeleteUser()
//         {
//             IServiceCollection services = BuildDependencies();
//             using (ServiceProvider serviceProvider = services.BuildServiceProvider())
//             {
//                 var userController = serviceProvider.GetRequiredService<UserController>();

//                 var testUser = new UserDto
//                 {
//                     Id = Guid.Empty,
//                     UserName = "pepe",
//                     Password = "12345678",
//                     FirstName = "Pepe",
//                     LastName = "Luis",
//                     Email = "pepeluis@rmail.com",
//                 };
//                 string updatedPassword = "password";

//                 var actionResult = await userController.PostUser(testUser);

//                 var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 var user = Assert.IsType<UserDto>(okObjectResult.Value);

//                 actionResult = await userController.GetUsers();

//                 okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 var users = Assert.IsType<List<UserDto>>(okObjectResult.Value);
//                 Assert.NotEmpty(users);

//                 actionResult = await userController.GetUser(user.Id);

//                 okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 user = Assert.IsType<UserDto>(okObjectResult.Value);
//                 Assert.Equal(testUser.UserName, user.UserName);
//                 Assert.Equal(testUser.Password, user.Password);
//                 Assert.Equal(testUser.FirstName, user.FirstName);
//                 Assert.Equal(testUser.LastName, user.LastName);
//                 Assert.Equal(testUser.Email, user.Email);

//                 user.Password = updatedPassword;
//                 actionResult = await userController.PutUser(user);

//                 okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 user = Assert.IsType<UserDto>(okObjectResult.Value);
//                 actionResult = await userController.GetUser(user.Id);
//                 okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 user = Assert.IsType<UserDto>(okObjectResult.Value);
//                 Assert.Equal(updatedPassword, user.Password);

//                 actionResult = await userController.DeleteUser(user.Id);

//                 okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
//                 var value = Assert.IsType<bool>(okObjectResult.Value);
//                 Assert.True(value);
//             }
//         }

//         private IServiceCollection BuildDependencies()
//         {
//             IServiceCollection services = new ServiceCollection();
//             services.AddScoped<IUserRepository, UserRepositoryMock>();
//             services.AddScoped<IUnitOfWork, UnitOfWorkMock>();
//             services.AddScoped<INotificationService, NotificationHTMLService>();
//             services.AddScoped<IUserService, UserService>();
//             services.AddScoped<UserController>();

//             return services;
//         }

//     }
// }
