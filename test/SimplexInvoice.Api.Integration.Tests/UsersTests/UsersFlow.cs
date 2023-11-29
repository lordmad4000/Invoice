using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Models.Response;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.UsersTests;

public class UsersFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public UsersFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Register_User_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();
        UserUpdateRequest userUpdateRequest = GetUserUpdateRequest();

        //Act
        var response = await client.PostAsync("/api/Users/Register", JsonContent.Create(userRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserRegisterRequest().Email, user.Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, user.LastName);

        //Act
        userUpdateRequest.Id = user.Id;
        response = await client.PutAsync("/api/Users/Update", JsonContent.Create(userUpdateRequest));
        content = await response.Content.ReadAsStringAsync();
        user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserUpdateRequest().Email, user.Email);
        Assert.Equal(GetUserUpdateRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserUpdateRequest().LastName, user.LastName);

        //Act
        response = await client.DeleteAsync($"/api/Users/Delete/{user.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_User_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Users/Register", JsonContent.Create(userRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserRegisterRequest().Email, user.Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, user.LastName);

        //Act
        response = await client.GetAsync($"/api/Users/GetById{user.Id}");
        content = await response.Content.ReadAsStringAsync();
        user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserRegisterRequest().Email, user.Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, user.LastName);

        //Act
        response = await client.DeleteAsync($"/api/Users/Delete/{user.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_User_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Users/Register", JsonContent.Create(userRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserRegisterRequest().Email, user.Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, user.LastName);

        //Act
        response = await client.GetAsync($"/api/Users/GetAll");
        content = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<UserResponse>>(content);
        if (users is null)
            users = new List<UserResponse>();

        //Assert
        Assert.NotEmpty(users);

        //Act
        response = await client.DeleteAsync($"/api/Users/Delete/{user.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_User_Then_GetLast_1_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        UserRegisterRequest userRegisterRequest = GetUserRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/Users/Register", JsonContent.Create(userRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(content);
        if (user is null)
            user = new UserResponse();

        //Assert
        Assert.Equal(GetUserRegisterRequest().Email, user.Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, user.FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, user.LastName);

        //Act
        response = await client.GetAsync($"/api/Users/GetLast1");
        content = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<List<UserResponse>>(content);
        if (users is null)
            users = new List<UserResponse>();

        //Assert
        Assert.Single(users);
        Assert.Equal(GetUserRegisterRequest().Email, users.First().Email);
        Assert.Equal(GetUserRegisterRequest().FirstName, users.First().FirstName);
        Assert.Equal(GetUserRegisterRequest().LastName, users.First().LastName);

        //Act
        response = await client.DeleteAsync($"/api/Users/Delete/{user.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
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
            Email = "testmod@testmod.com",
            Password = "testmodpas",
            FirstName = "TEST FIRSTNAME MOD",
            LastName = "TEST LASTNAME MOD",
        };

}