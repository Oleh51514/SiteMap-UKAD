using System.Collections.Generic;

namespace Sitemap.BLL.Abstracts.Services
{

    public interface IBaseService<TDto, TKey> where TDto : class
    {
        TDto Get(TKey id);
        IEnumerable<TDto> GetAll();
        TDto Add(TDto data);

    }   
}
