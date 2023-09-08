using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.IdDocumentTypes.Commands;
using SimplexInvoice.Application.Products.Commands;
using SimplexInvoice.Application.TaxRates.Commands;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;
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
            CreateMap<IdDocumentTypeDto, IdDocumentTypeUpdateCommand>();

            CreateMap<TaxRateDto, TaxRate>();
            CreateMap<TaxRate, TaxRateDto>();
            CreateMap<TaxRateDto, TaxRateUpdateCommand>();

            CreateMap<Product, ProductDto>().ForMember(c => c.Price, 
                                                       x => x.MapFrom(src => src.UnitPrice.Amount))
                                            .ForMember(c => c.Currency,
                                                       x => x.MapFrom(src => src.UnitPrice.Currency));
            CreateMap<ProductDto, Product>().ForMember(c => c.UnitPrice,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.Price)));
            CreateMap<ProductDto, ProductUpdateCommand>();

        }
    }

}