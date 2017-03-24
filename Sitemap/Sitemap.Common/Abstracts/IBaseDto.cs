using System;

namespace Sitemap.Common.Abstracts
{
    public interface IBaseDto<TKey>
    {
        TKey Id { get; set; }
    }
}
