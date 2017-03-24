using AutoMapper;
using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common.Abstracts;
using Sitemap.DAL.Abstracts;
using Sitemap.DAL.Abstracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Sitemap.BLL.Services
{
    public abstract class BaseService<TRepository, TDto, TEntity, TKey, TMapper> : IBaseService<TDto, TKey>
         where TRepository : IRepository
         where TDto : class, IBaseDto<TKey>
         where TEntity : class, IBaseEntity<TKey>
         where TMapper : IMapper
    {

        protected readonly TRepository repository;
        protected readonly IMapper mapper;
        protected BaseService(TRepository repository, TMapper modelMapper)
        {
            this.mapper = modelMapper;
            this.repository = repository;
        }
                
        protected virtual IQueryable<TEntity> PrefilteredQuery
        {
            get { return repository.Get<TEntity>(); }
        }

        public virtual TDto Add(TDto data)
        {
            var entity = mapper.Map<TDto, TEntity>(data);
            this.repository.Save<TEntity>(entity);
            data.Id = entity.Id;
            return mapper.Map<TEntity, TDto>(entity);
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(PrefilteredQuery);
            return result;
        }

        public virtual TDto Get(TKey id)
        {
            var value = PrefilteredQuery.Where("Id = @0", id).FirstOrDefault();
            var result = mapper.Map<TEntity, TDto>(value);
            return result;
        }               
    }
}
