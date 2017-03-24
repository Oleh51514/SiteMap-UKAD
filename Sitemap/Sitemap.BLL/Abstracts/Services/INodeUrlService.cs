using Sitemap.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.BLL.Abstracts.Services
{
    public interface INodeUrlService : IBaseService<NodeUrlDto, int>
    {
        /// <summary>
        /// Get all node url by id.
        /// </summary>
        ///<param name="id">ID of selected domain.</param>
        /// <returns>NodeUrlDto list by ID.</returns>
        IEnumerable<NodeUrlDto> GetAllById(int id);

    }
}
