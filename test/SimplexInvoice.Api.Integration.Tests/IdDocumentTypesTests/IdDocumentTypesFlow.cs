using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.IdDocumentTypesTests;

public class IdDocumentTypesFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public IdDocumentTypesFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Register_IdDocumentType_Then_ModifyIt_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        IdDocumentTypeRegisterRequest idDocumentTypeRegisterRequest = GetIdDocumentTypeRegisterRequest();
        IdDocumentTypeDto idDocumentTypeDto = GetIdDocumentTypeDto();

        //Act
        var response = await client.PostAsync("/api/IdDocumentTypes/Register", JsonContent.Create(idDocumentTypeRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var idDocumentType = JsonConvert.DeserializeObject<IdDocumentTypeDto>(content);
        if (idDocumentType is null)
            idDocumentType = new IdDocumentTypeDto();

        //Assert
        Assert.Equal(GetIdDocumentTypeRegisterRequest().Name, idDocumentType.Name);

        //Act
        idDocumentTypeDto.Id = idDocumentType.Id;
        response = await client.PutAsync("/api/IdDocumentTypes/Update", JsonContent.Create(idDocumentTypeDto));
        content = await response.Content.ReadAsStringAsync();
        idDocumentType = JsonConvert.DeserializeObject<IdDocumentTypeDto>(content);
        if (idDocumentType is null)
            idDocumentType = new IdDocumentTypeDto();

        //Assert
        Assert.Equal(GetIdDocumentTypeDto().Name, idDocumentType.Name);

        //Act
        response = await client.DeleteAsync($"/api/IdDocumentTypes/Delete/{idDocumentType.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_IdDocumentType_Then_GetById_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        IdDocumentTypeRegisterRequest idDocumentTypeRegisterRequest = GetIdDocumentTypeRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/IdDocumentTypes/Register", JsonContent.Create(idDocumentTypeRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var idDocumentType = JsonConvert.DeserializeObject<IdDocumentTypeDto>(content);
        if (idDocumentType is null)
            idDocumentType = new IdDocumentTypeDto();

        //Assert
        Assert.Equal(GetIdDocumentTypeRegisterRequest().Name, idDocumentType.Name);

        //Act
        response = await client.GetAsync($"/api/IdDocumentTypes/GetById{idDocumentType.Id}");
        content = await response.Content.ReadAsStringAsync();
        idDocumentType = JsonConvert.DeserializeObject<IdDocumentTypeDto>(content);
        if (idDocumentType is null)
            idDocumentType = new IdDocumentTypeDto();

        //Assert
        Assert.Equal(GetIdDocumentTypeRegisterRequest().Name, idDocumentType.Name);

        //Act
        response = await client.DeleteAsync($"/api/IdDocumentTypes/Delete/{idDocumentType.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Register_IdDocumentType_Then_GetAll_Then_RemoveIt()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        IdDocumentTypeRegisterRequest idDocumentTypeRegisterRequest = GetIdDocumentTypeRegisterRequest();

        //Act
        var response = await client.PostAsync("/api/IdDocumentTypes/Register", JsonContent.Create(idDocumentTypeRegisterRequest));
        var content = await response.Content.ReadAsStringAsync();
        var idDocumentType = JsonConvert.DeserializeObject<IdDocumentTypeDto>(content);
        if (idDocumentType is null)
            idDocumentType = new IdDocumentTypeDto();

        //Assert
        Assert.Equal(GetIdDocumentTypeRegisterRequest().Name, idDocumentType.Name);

        //Act
        response = await client.GetAsync($"/api/IdDocumentTypes/GetAll");
        content = await response.Content.ReadAsStringAsync();
        var idDocumentTypes = JsonConvert.DeserializeObject<List<IdDocumentTypeDto>>(content);
        if (idDocumentTypes is null)
            idDocumentTypes = new List<IdDocumentTypeDto>();

        //Assert
        Assert.NotEmpty(idDocumentTypes);

        //Act
        response = await client.DeleteAsync($"/api/IdDocumentTypes/Delete/{idDocumentType.Id}");
        content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<bool>(content);

        //Assert
        Assert.True(result);
    }

    private IdDocumentTypeRegisterRequest GetIdDocumentTypeRegisterRequest() =>
        new IdDocumentTypeRegisterRequest
        {
            Name = "TEST"
        };

    private IdDocumentTypeDto GetIdDocumentTypeDto() =>
        new IdDocumentTypeDto
        {
            Name = "TEST MOD"
        };

}