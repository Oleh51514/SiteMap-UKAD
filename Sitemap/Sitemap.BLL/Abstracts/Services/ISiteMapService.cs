using Sitemap.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.BLL.Abstracts.Services
{
    public interface ISiteMapService
    {

        int DepthOfUrlsSearch { get; set; }

        /// <summary>
        /// Measurement site map.
        /// </summary>
        /// <param name="domain">User enter domain url.</param>
        /// <param name="IdDomain">ID of get domain.</param>
        void MeasurementSiteMap(string domain, int IdDomain);

        /// <summary>
        /// Get tree grid of site map.
        /// </summary>
        ///<param name="domains">List node urls site.</param>
        /// <returns>Tree grid stucture NodeUrlForGridDto list.</returns>
        NodeUrlForGridDto[] GenerateSitemap(IEnumerable<NodeUrlDto> domains);
    }
}
