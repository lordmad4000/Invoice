using AutoMapper;
using Invoice.Application.AutoMapper;

namespace Invoice.Api.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile(new DtoToResponseMappingProfile());
                cfg.AddProfile(new RequestToCommandMappingProfile());
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
        }
    }
}