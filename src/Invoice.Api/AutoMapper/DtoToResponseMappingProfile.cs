using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Api.Models.Response;
using Invoice.Application.Models;

namespace Invoice.Api.AutoMapper
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