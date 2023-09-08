using AutoMapper;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Api.AutoMapper
{
    public class RequestToDtoMappingProfile : Profile
    {
        public RequestToDtoMappingProfile()
        {
            CreateMap<UserRegisterRequest, UserDto>();
            CreateMap<UserUpdateRequest, UserDto>();
            CreateMap<TaxRateRegisterRequest, TaxRateDto>();
            CreateMap<ProductRegisterRequest, ProductDto>();
        }
    }

}