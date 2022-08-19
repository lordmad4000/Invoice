using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Application.CQRS.Authentication.Commands.Register;
using Invoice.Application.CQRS.Users.Commands.Register;

namespace Invoice.Api.AutoMapper
{
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<UserRegisterRequest, AuthenticationRegisterCommand>();
            CreateMap<UserUpdateRequest, UserUpdateCommand>().ConstructUsing(x => new UserUpdateCommand(x.Id, x.Email, x.Password, x.FirstName, x.LastName));
        }
    }

}