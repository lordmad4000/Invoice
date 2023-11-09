using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Domain.Companies;
using SimplexInvoice.Domain.Customers;
using SimplexInvoice.Domain.IdDocumentTypes;
using SimplexInvoice.Domain.Invoices;
using SimplexInvoice.Domain.Products;
using SimplexInvoice.Domain.TaxRates;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.ValueObjects;
using System;

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

            CreateMap<Customer, CustomerDto>().ForMember(c => c.Street,
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

            CreateMap<CustomerDto, Customer>().ForMember(c => c.Address,
                                                         x => x.MapFrom(src => new Address(src.Street, src.City, src.State, src.Country, src.PostalCode)))
                                              .ForMember(c => c.Phone,
                                                         x => x.MapFrom(src => new PhoneNumber(src.Phone)))
                                              .ForMember(c => c.EmailAddress,
                                                         x => x.MapFrom(src => new EmailAddress(src.Email)));

            CreateMap<InvoiceLine, InvoiceLineDto>().ForMember(c => c.Price,
                                                               x => x.MapFrom(src => src.Price.Amount))
                                                    .ForMember(c => c.Currency,
                                                               x => x.MapFrom(src => src.Price.Currency))
                                                    .ForMember(c => c.Tax,
                                                               x => x.MapFrom(src => src.Tax.Amount))
                                                    .ForMember(c => c.Discount,
                                                               x => x.MapFrom(src => src.Discount.Amount))
                                                    .ForMember(c => c.TaxBase,
                                                               x => x.MapFrom(src => src.TaxBase.Amount))
                                                    .ForMember(c => c.Total,
                                                               x => x.MapFrom(src => src.Total.Amount));

            CreateMap<InvoiceLineDto, InvoiceLine>().ForMember(c => c.Price,
                                                               x => x.MapFrom(src => new Money(src.Currency, src.Price)))
                                                    .ForMember(c => c.Tax,
                                                               x => x.Ignore())
                                                    .ForMember(c => c.Discount,
                                                               x => x.Ignore())
                                                    .ForMember(c => c.TaxBase,
                                                               x => x.Ignore())
                                                    .ForMember(c => c.Total,
                                                               x => x.Ignore());

            CreateMap<Invoice, InvoiceDto>().ForMember(c => c.Date,
                                                       x => x.MapFrom(src => DateOnly.FromDateTime(src.Date)))
                                            .ForMember(c => c.CompanyStreet,
                                                       x => x.MapFrom(src => src.CompanyAddress.Street))
                                            .ForMember(c => c.CompanyCity,
                                                       x => x.MapFrom(src => src.CompanyAddress.City))
                                            .ForMember(c => c.CompanyState,
                                                       x => x.MapFrom(src => src.CompanyAddress.State))
                                            .ForMember(c => c.CompanyCountry,
                                                       x => x.MapFrom(src => src.CompanyAddress.Country))
                                            .ForMember(c => c.CompanyPostalCode,
                                                       x => x.MapFrom(src => src.CompanyAddress.PostalCode))
                                            .ForMember(c => c.CompanyPhone,
                                                       x => x.MapFrom(src => src.CompanyPhoneNumber.Phone))
                                            .ForMember(c => c.CompanyEmail,
                                                       x => x.MapFrom(src => src.CompanyEmailAddress.Address))
                                            .ForMember(c => c.CustomerStreet,
                                                       x => x.MapFrom(src => src.CustomerAddress.Street))
                                            .ForMember(c => c.CustomerCity,
                                                       x => x.MapFrom(src => src.CustomerAddress.City))
                                            .ForMember(c => c.CustomerState,
                                                       x => x.MapFrom(src => src.CustomerAddress.State))
                                            .ForMember(c => c.CustomerCountry,
                                                       x => x.MapFrom(src => src.CustomerAddress.Country))
                                            .ForMember(c => c.CustomerPostalCode,
                                                       x => x.MapFrom(src => src.CustomerAddress.PostalCode))
                                            .ForMember(c => c.CustomerPhone,
                                                       x => x.MapFrom(src => src.CustomerPhoneNumber.Phone))
                                            .ForMember(c => c.CustomerEmail,
                                                       x => x.MapFrom(src => src.CustomerEmailAddress.Address))
                                            .ForMember(c => c.TotalTax,
                                                       x => x.MapFrom(src => src.TotalTax.Amount))
                                            .ForMember(c => c.TotalDiscount,
                                                       x => x.MapFrom(src => src.TotalDiscount.Amount))
                                            .ForMember(c => c.TotalTaxBase,
                                                       x => x.MapFrom(src => src.TotalTaxBase.Amount))
                                            .ForMember(c => c.Total,
                                                       x => x.MapFrom(src => src.Total.Amount))
                                            .ForMember(c => c.Currency,
                                                       x => x.MapFrom(src => src.Total.Currency));

            CreateMap<InvoiceDto, Invoice>().ForMember(c => c.Date,
                                                       x => x.MapFrom(src => src.Date.ToDateTime(TimeOnly.MinValue)))
                                            .ForMember(c => c.CompanyAddress,
                                                       x => x.MapFrom(src => new Address(src.CompanyStreet, src.CompanyCity, src.CompanyState, src.CompanyCountry, src.CompanyPostalCode)))
                                            .ForMember(c => c.CompanyPhoneNumber,
                                                       x => x.MapFrom(src => new PhoneNumber(src.CompanyPhone)))
                                            .ForMember(c => c.CompanyEmailAddress,
                                                       x => x.MapFrom(src => new EmailAddress(src.CompanyEmail)))
                                            .ForMember(c => c.CustomerAddress,
                                                       x => x.MapFrom(src => new Address(src.CustomerStreet, src.CustomerCity, src.CustomerState, src.CustomerCountry, src.CustomerPostalCode)))
                                            .ForMember(c => c.CustomerPhoneNumber,
                                                       x => x.MapFrom(src => new PhoneNumber(src.CustomerPhone)))
                                            .ForMember(c => c.CustomerEmailAddress,
                                                       x => x.MapFrom(src => new EmailAddress(src.CustomerEmail)))
                                            .ForMember(c => c.TotalTax,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.TotalTax)))
                                            .ForMember(c => c.TotalDiscount,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.TotalDiscount)))
                                            .ForMember(c => c.TotalTaxBase,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.TotalTaxBase)))
                                            .ForMember(c => c.Total,
                                                       x => x.MapFrom(src => new Money(src.Currency, src.Total)));

            CreateMap<TotalTax, TotalTaxDto>();
            CreateMap<TotalTaxDto, TotalTax>();

        }
    }

}