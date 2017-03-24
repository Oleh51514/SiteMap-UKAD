using AutoMapper;
using Sitemap.Common.DTO;
using Sitemap.DAL.Entities;

namespace Sitemap.Bootstrap.AutoMapper
{
    class AutoMapperProfile : Profile
    {
        private readonly IMapperConfiguration MappingConfiguration;
        public AutoMapperProfile(IMapperConfiguration config)
        {
            MappingConfiguration = config;
        }

        protected override void Configure()
        {
            EntityToDto();
            DtoToEntity();
        }

        private void EntityToDto()
        {
            MappingConfiguration.CreateMap<NodeUrl, NodeUrlDto>();
            MappingConfiguration.CreateMap<Domain, DomainDto>();
        }
        private void DtoToEntity()
        {
            MappingConfiguration.CreateMap<NodeUrlDto, NodeUrl>();
            MappingConfiguration.CreateMap<DomainDto, Domain>();
        }

    }
}
