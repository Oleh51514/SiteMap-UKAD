using AutoMapper;
using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common.DTO;
using Sitemap.DAL.Abstracts.Repositories;
using Sitemap.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sitemap.BLL.Services
{
    public class NodeUrlService : BaseService<IRepository, NodeUrlDto, NodeUrl, int, IMapper>, INodeUrlService
    {

        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        public NodeUrlService(IRepository repository, IMapper modelMapper)
            : base(repository, modelMapper)
        {
        }

        /// <summary>
        /// Get all node url by id.
        /// </summary>
        ///<param name="id">ID of selected domain.</param>
        /// <returns>NodeUrlDto list by ID.</returns>
        public IEnumerable<NodeUrlDto> GetAllById(int id)
        {
            var list = PrefilteredQuery.Where(x => x.DomainId == id).OrderBy(x => x.Url);
            var resultList = mapper.Map<IEnumerable<NodeUrl>, IEnumerable<NodeUrlDto>>(list);
            return resultList;
        }
    }
}
