using AutoMapper;
using Invoice.Api.Models.Request;
using Invoice.Application.CQRS.Authentication.Commands.Register;

namespace Invoice.Api.AutoMapper
{
    public class RequestToCommandMappingProfile : Profile
    {
        public RequestToCommandMappingProfile()
        {
            CreateMap<UserRegisterRequest, AuthenticationRegisterCommand>();
        }
    }

}