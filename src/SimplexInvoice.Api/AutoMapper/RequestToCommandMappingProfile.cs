using AutoMapper;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Authentication.Commands;
using SimplexInvoice.Application.Products.Commands;
using SimplexInvoice.Application.TaxRates.Commands;
using SimplexInvoice.Application.Users.Commands;

namespace SimplexInvoice.Api.AutoMapper
{
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<UserRegisterRequest, AuthenticationRegisterCommand>();
            CreateMap<UserRegisterRequest, UserRegisterCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>().ConstructUsing(x => new UserUpdateCommand(x.Id, x.Email, x.Password, x.FirstName, x.LastName));
            CreateMap<TaxRateRegisterRequest, TaxRateRegisterCommand>();
            CreateMap<ProductRegisterRequest, ProductRegisterCommand>();
        }
    }

}