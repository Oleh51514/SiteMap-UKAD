using System;

namespace Sitemap.DAL.Abstracts
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
