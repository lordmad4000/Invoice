using AutoMapper;
using Users.API.Models.Response;
using Users.Application.Models;

namespace Users.API.AutoMapper
{
    public class DtoToResponseMappingProfile : Profile
    {
        public DtoToResponseMappingProfile()
        {
            CreateMap<UserDto, UserResponse>();
        }
    }

}