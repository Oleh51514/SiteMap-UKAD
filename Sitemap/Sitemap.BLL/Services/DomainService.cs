using System;
using AutoMapper;
using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common.DTO;
using Sitemap.DAL.Abstracts.Repositories;
using Sitemap.DAL.Entities;

namespace Sitemap.BLL.Services
{
    public class DomainService : BaseService<IRepository, DomainDto, Domain, int, IMapper>, IDomainService
    {
        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        public DomainService(IRepository repository, IMapper modelMapper)
            : base(repository, modelMapper)
        {
        }

        /// <summary>
        /// Get string correct domain.
        /// </summary>
        ///<param name="url">domain get from user.</param>
        /// <returns>string correct domain.</returns>
        public string GetCorrectfDomain(string url)
        {
            Uri uri = new Uri(url);
            return uri.AbsoluteUri.Trim(uri.AbsolutePath.ToCharArray());
        }
    }
}
