using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SimplexInvoice.Api.Integration.Tests.Common;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;
using System.Net.Http.Json;

namespace SimplexInvoice.Api.Integration.Tests.CompaniesTests;

public class CompaniesFlow : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public CompaniesFlow(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async Task Modify_Company_Then_Restore()
    {
        //Arrange
        var client = WebApplicationFactoryHelper.GetHttpClient(_applicationFactory);
        var oldCompany = await GetCompany(client);
        CompanyRegisterUpdateRequest companyRegisterUpdateRequest = GetCompanyRegisterUpdateRequest();
        companyRegisterUpdateRequest.IdDocumentTypeId = oldCompany.IdDocumentTypeId;

        //Act
        var response = await client.PutAsync("/api/Companies/Update", JsonContent.Create(companyRegisterUpdateRequest));
        var content = await response.Content.ReadAsStringAsync();
        var company = JsonConvert.DeserializeObject<CompanyDto>(content);
        if (company is null)
            company = new CompanyDto();

        //Assert
        Assert.Equal(GetCompanyRegisterUpdateRequest().Name, company.Name);
        Assert.Equal(GetCompanyRegisterUpdateRequest().IdDocumentNumber, company.IdDocumentNumber);
        Assert.Equal(GetCompanyRegisterUpdateRequest().Street, company.Street);
        Assert.Equal(GetCompanyRegisterUpdateRequest().City, company.City);
        Assert.Equal(GetCompanyRegisterUpdateRequest().State, company.State);
        Assert.Equal(GetCompanyRegisterUpdateRequest().Country, company.Country);
        Assert.Equal(GetCompanyRegisterUpdateRequest().PostalCode, company.PostalCode);
        Assert.Equal(GetCompanyRegisterUpdateRequest().Phone, company.Phone);
        Assert.Equal(GetCompanyRegisterUpdateRequest().Email, company.Email);

        //Act
        response = await client.PutAsync("/api/Companies/Update", JsonContent.Create(oldCompany));
        content = await response.Content.ReadAsStringAsync();
        company = JsonConvert.DeserializeObject<CompanyDto>(content);
        if (company is null)
            company = new CompanyDto();

        //Assert
        Assert.Equal(oldCompany.Name, company.Name);
        Assert.Equal(oldCompany.IdDocumentTypeId, company.IdDocumentTypeId);
        Assert.Equal(oldCompany.IdDocumentNumber, company.IdDocumentNumber);
        Assert.Equal(oldCompany.Street, company.Street);
        Assert.Equal(oldCompany.City, company.City);
        Assert.Equal(oldCompany.State, company.State);
        Assert.Equal(oldCompany.Country, company.Country);
        Assert.Equal(oldCompany.PostalCode, company.PostalCode);
        Assert.Equal(oldCompany.Phone, company.Phone);
        Assert.Equal(oldCompany.Email, company.Email);
    }

    private async Task<CompanyRegisterUpdateRequest> GetCompany(HttpClient client)
    {
        var response = await client.GetAsync($"/api/Companies/Get");
        var content = await response.Content.ReadAsStringAsync();
        var companyDto = JsonConvert.DeserializeObject<CompanyDto>(content);
        if (companyDto is null)
            companyDto = new CompanyDto();

        return new CompanyRegisterUpdateRequest
        {
            Name = companyDto.Name,
            IdDocumentTypeId = companyDto.IdDocumentTypeId,
            IdDocumentNumber = companyDto.IdDocumentNumber,
            Street = companyDto.Street,
            City = companyDto.City,
            State = companyDto.State,
            Country = companyDto.Country,
            PostalCode = companyDto.PostalCode,
            Phone = companyDto.Phone,
            Email = companyDto.Email
        };
    }

    private CompanyRegisterUpdateRequest GetCompanyRegisterUpdateRequest() =>
            new CompanyRegisterUpdateRequest
            {
                Name = "TEST NAME",
                IdDocumentNumber = "TEST ID DOCUMENT NUMBER",
                Street = "TEST STREET",
                City = "TEST CITY",
                State = "TEST STATE",
                Country = "TEST COUNTRY",
                PostalCode = "TEST POSTAL CODE",
                Phone = "+01 718 222 2222",
                Email = "test@test.com"
            };

}