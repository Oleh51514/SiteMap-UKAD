using Sitemap.DAL.Abstracts.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Sitemap.DAL.Repositories
{
    public class Repository : IRepository
    {
        protected readonly SitemapDbContext context;

        public Repository(SitemapDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> Get<T>() where T : class
        {
            return this.context.Set<T>();
        }

        public virtual int GetCount<T>() where T : class
        {
            return this.context.Set<T>().Count();
        }

        public virtual T Get<T>(object id) where T : class
        {
            return this.context.Set<T>().Find(id);
        }

        public virtual void Delete<T>(object id) where T : class
        {
            this.context.Set<T>().Remove(this.context.Set<T>().Find(id));
            this.context.SaveChanges();
        }

        public void Save<T>(T data) where T : class
        {            
            if (!context.Set<T>().Local.Any(d => d == data))
            {
                this.context.Set<T>().Add(data);
                this.context.Entry(data).State = EntityState.Added;
            }
            else
            {
                this.context.Entry(data).State = EntityState.Modified;
            }
            this.context.SaveChanges();
        }
    }
}
