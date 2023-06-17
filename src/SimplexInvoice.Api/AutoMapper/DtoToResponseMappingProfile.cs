using AutoMapper;
using SimplexInvoice.Api.Models.Request;
using SimplexInvoice.Api.Models.Response;
using SimplexInvoice.Application.Common.Dto;

namespace SimplexInvoice.Api.AutoMapper
{
    public class DtoToResponseMappingProfile : Profile
    {
        public DtoToResponseMappingProfile()
        {
            CreateMap<UserDto, UserResponse>();
            CreateMap<UserUpdateRequest, UserDto>();
            CreateMap<UserRegisterRequest, UserDto>();
        }
    }

}