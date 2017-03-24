using AutoMapper;

namespace Sitemap.Bootstrap.AutoMapper
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig(IMapperConfiguration mapperConfiguration)
        {
            Configure(mapperConfiguration);
        }
        public void Configure(IMapperConfiguration mapperConfiguration)
        {
            mapperConfiguration.AddProfile(new AutoMapperProfile(mapperConfiguration));
        }
    }
}
