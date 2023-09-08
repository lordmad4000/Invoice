using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.ValueObjects;

namespace SimplexInvoice.Application.AutoMapper
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(c => c.Email, 
                                                 x => x.MapFrom(src => src.EmailAddress));
            CreateMap<UserDto, User>().ForMember(c => c.EmailAddress, 
                                                 x => x.MapFrom(src => new EmailAddress(src.Email)));

            CreateMap<IdDocumentTypeDto, IdDocumentType>();
            CreateMap<IdDocumentType, IdDocumentTypeDto>();

            CreateMap<TaxRateDto, TaxRate>();
            CreateMap<TaxRate, TaxRateDto>();

            CreateMap<Product, ProductDto>().ForMember(c => c.Price, 
                                                       x => x.MapFrom(src => src.UnitPrice.Amount))
                                            .ForMember(c => c.Currency,
                                                       x => x.MapFrom(src => src.UnitPrice.Currency));
            CreateMap<ProductDto, Product>().ForMember(c => c.UnitPrice,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.Price)));

            CreateMap<Company, CompanyDto>().ForMember(c => c.Street,
                                                       x => x.MapFrom(src => src.Address.Street))
                                            .ForMember(c => c.Street,
                                                       x => x.MapFrom(src => src.Address.Street))
                                            .ForMember(c => c.City,
                                                       x => x.MapFrom(src => src.Address.City))
                                            .ForMember(c => c.State,
                                                       x => x.MapFrom(src => src.Address.State))
                                            .ForMember(c => c.Country,
                                                       x => x.MapFrom(src => src.Address.Country))
                                            .ForMember(c => c.PostalCode,
                                                       x => x.MapFrom(src => src.Address.PostalCode))
                                            .ForMember(c => c.Phone,
                                                       x => x.MapFrom(src => src.Phone.Phone))
                                            .ForMember(c => c.Email,
                                                       x => x.MapFrom(src => src.EmailAddress.Address));

            CreateMap<CompanyDto, Company>().ForMember(c => c.Address,
                                                       x => x.MapFrom(src => new Address(src.Street, src.City, src.State, src.Country, src.PostalCode)))
                                            .ForMember(c => c.Phone,
                                                       x => x.MapFrom(src => new PhoneNumber(src.Phone)))
                                            .ForMember(c => c.EmailAddress,
                                                       x => x.MapFrom(src => new EmailAddress(src.Email)));

        }
    }

}