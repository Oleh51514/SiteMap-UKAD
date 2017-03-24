using System.Linq;

namespace Sitemap.DAL.Abstracts.Repositories
{
    public interface IRepository
    {
        IQueryable<T> Get<T>() where T : class;
        int GetCount<T>() where T : class;
        T Get<T>(object id) where T : class;
        void Delete<T>(object id) where T : class;
        void Save<T>(T data) where T : class;
    }
}
