using AutoMapper;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Api.AutoMapper
{
    public class ResponseToDtoMappingProfile : Profile
    {
        public ResponseToDtoMappingProfile()
        {
            CreateMap<UserResponse, UserDto>();
            CreateMap<UserDto, UserResponse>();            
        }
    }

}