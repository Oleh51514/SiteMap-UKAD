using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.BLL.Abstracts.Services
{
    public interface IUrlsParse
    {
        /// <summary>
        /// Get all urls from site.
        /// </summary>
        ///<param name="url">Url from site.</param>
        /// <returns>Urls list of site.</returns>
        List<string> GetListUrlsOfSiteMap(string url);

        /// <summary>
        /// Search url on page.
        /// </summary>
        ///<param name="url">Url from site.</param>
        void SearchUrlOnPage(string url);
    }
}
