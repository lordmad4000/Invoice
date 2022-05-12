using AutoMapper;

namespace Users.API.AutoMapper
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
            });
        }
    }
}