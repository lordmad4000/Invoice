using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Application.CQRS.Authentication.Commands;
using Invoice.Application.CQRS.Users.Commands;

namespace Invoice.Api.AutoMapper
{
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<UserRegisterRequest, AuthenticationRegisterCommand>();
            CreateMap<UserRegisterRequest, UserRegisterCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>().ConstructUsing(x => new UserUpdateCommand(x.Id, x.Email, x.Password, x.FirstName, x.LastName));
        }
    }

}