using AutoMapper;
using Invoice.Application.Common.Dto;
using Invoice.Domain.Entities;
using Invoice.Domain.Users;
using Invoice.Domain.ValueObjects;

namespace Invoice.Application.AutoMapper
{
    public class EntityDtoMappingProfile : Profile
    {
        public EntityDtoMappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(c => c.Email, 
                                                 x => x.MapFrom(src => src.EmailAddress.ToString()));
            CreateMap<UserDto, User>().ForMember(c => c.EmailAddress, 
                                                 x => x.MapFrom(src => new EmailAddress(src.Email)));

            CreateMap<IdDocumentTypeDto, IdDocumentType>();
            CreateMap<IdDocumentType, IdDocumentTypeDto>();
        }
    }

}