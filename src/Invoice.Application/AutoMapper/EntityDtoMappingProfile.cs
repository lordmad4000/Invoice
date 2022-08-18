using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Application.Services.Configuration.Common;
using Invoice.Domain.Entities;
using Invoice.Domain.ValueObjects;

namespace Invoice.Api.AutoMapper
{
    public class EntityDtoMappingProfile : Profile
    {
        public EntityDtoMappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(c => c.Email, 
                                                 x => x.MapFrom(src => src.EmailAddress.ToString()));
            CreateMap<UserDto, User>().ForMember(c => c.EmailAddress, 
                                                 x => x.MapFrom(src => new EmailAddress(src.Email)));

            CreateMap<IdDocumentTypeResult, IdDocumentType>();
            CreateMap<IdDocumentType, IdDocumentTypeResult>();
        }
    }

}