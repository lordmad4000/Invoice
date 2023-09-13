using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Companies.Commands;
using SimplexInvoice.Application.Customers.Commands;
using SimplexInvoice.Application.IdDocumentTypes.Commands;
using SimplexInvoice.Application.Products.Commands;
using SimplexInvoice.Application.TaxRates.Commands;

namespace SimplexInvoice.Application.AutoMapper
{
    public class DtoToCommandMappingProfile : Profile
    {
        public DtoToCommandMappingProfile()
        {
            CreateMap<IdDocumentTypeDto, IdDocumentTypeUpdateCommand>();
            CreateMap<TaxRateDto, TaxRateUpdateCommand>();
            CreateMap<ProductDto, ProductUpdateCommand>();
            CreateMap<CompanyDto, CompanyUpdateCommand>();
            CreateMap<CustomerDto, CustomerUpdateCommand>();
        }
    }

}