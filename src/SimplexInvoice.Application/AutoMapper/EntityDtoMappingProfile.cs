using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.ValueObjects;

namespace SimplexInvoice.Application.AutoMapper
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

            CreateMap<Product, ProductDto>().ForMember(c => c.Price, 
                                                       x => x.MapFrom(src => src.UnitPrice.Amount))
                                            .ForMember(c => c.Currency,
                                                       x => x.MapFrom(src => src.UnitPrice.Currency));
        }
    }

}