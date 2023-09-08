using AutoMapper;
using SimplexInvoice.Application.AutoMapper;

namespace SimplexInvoice.Api.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile(new RequestToCommandMappingProfile());
                cfg.AddProfile(new RequestToDtoMappingProfile());
                cfg.AddProfile(new ResponseToDtoMappingProfile());
                cfg.AddProfile(new EntityToDtoMappingProfile());
                cfg.AddProfile(new DtoToCommandMappingProfile());
            });
        }       
    }
}