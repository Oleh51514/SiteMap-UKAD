using Sitemap.Common.DTO;

namespace Sitemap.BLL.Abstracts.Services
{
    public interface IDomainService : IBaseService<DomainDto, int>
    {
        /// <summary>
        /// Get string correct domain.
        /// </summary>
        ///<param name="url">Domain get from user.</param>
        /// <returns>String correct domain.</returns>
        string GetCorrectfDomain(string url);
    }
}
