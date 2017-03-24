using Sitemap.BLL.Abstracts.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sitemap.BLL.Services
{
    public class UrlsParse : IUrlsParse
    {
        List<String> urlsPageList = new List<string>();
        string siteUrl;

        /// <summary>
        /// Get all urls from site.
        /// </summary>
        ///<param name="url">Url from site.</param>
        /// <returns>Urls list of site.</returns>
        public List<string> GetListUrlsOfSiteMap(string url)
        {
            siteUrl = url;
            SearchUrlOnPage(siteUrl);
            return urlsPageList;
        }

        /// <summary>
        /// Search url on page.
        /// </summary>
        ///<param name="url">Url from site.</param>
        public void SearchUrlOnPage(string url)
        {
            // Get htmlCode
            String htmlCode = GetHtmlCodeByUrl(url);
            if (htmlCode == null)
            return;
            // Take out a list of all links of the code page
            List<String> urlsListOnPage = GetUrlsListOnPage(htmlCode);
            List<String> urlsSelectedSite = FilterReferenceSite(urlsListOnPage);
            // Get distinct values from the list
            List<string> distinctUrlsList = urlsSelectedSite.Distinct().ToList().Where(x => !urlsPageList.Any(y => x == y)).ToList<String>();
            urlsPageList.AddRange(distinctUrlsList);
            foreach (var n in distinctUrlsList)
            {
                // call recursion method
                SearchUrlOnPage(n);
            }
        }
        /// <summary>
        /// Method deletes redundant char '/' from end of url
        /// </summary>
        ///<param name="url">url of site.</param>
        private void TrimUlr(string Url)
        {
            if (Url.EndsWith("/"))
                if (Url.EndsWith("/"))
                {
                    Url.TrimEnd('/');
                }
        }

        /// <summary>
        /// Get all urls  on page.
        /// </summary>
        ///<param name="html">Html of site page.</param>
        /// <returns>Urls list  on page.</returns>
        private List<string> GetUrlsListOnPage(string html)
        {
            // regular expressions for url
            Match m_links = Regex.Match(html, "<\\s*a\\s+href=\"(.*?)\"", RegexOptions.Multiline);
            List<string> allUrlOnPage = new List<string>();
            while (m_links.Success)
            {
                allUrlOnPage.Add(m_links.Groups[1].Value.ToString());
                m_links = m_links.NextMatch();
            }
            return allUrlOnPage;
        }

        /// <summary>
        /// Get all url being the copyright of this site.
        /// </summary>
        ///<param name="urlList">Urls list  on page.</param>
        /// <returns>Urls being the copyright of this site.</returns>
        private List<string> FilterReferenceSite(List<string> urlList)
        {
            // The list of references the pages of this site
            List<string> linksToRelatedSite = new List<string>();
            foreach (var item in urlList)
            {
                String link = item;
                Uri outUri;
                if (!(Uri.TryCreate(item, UriKind.Absolute, out outUri) && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps)))
                {
                    link = siteUrl + ((!link.StartsWith("/") && (!siteUrl.EndsWith("/"))) ? "/" : "") + link;
                }
                if (link.StartsWith(siteUrl))
                {
                    linksToRelatedSite.Add(TrimUrl(link));
                }
            }
            return linksToRelatedSite;
        }

        /// <summary>
        /// Trim url if he end with "/".
        /// </summary>
        ///<param name="url">Url of site.</param>
        /// <returns>Url without "/".</returns>
        public string TrimUrl(string url)
        {
            if (url.EndsWith("/"))
            {
                url = url.TrimEnd('/');
            }
            return url;
        }

        /// <summary>
        /// Get html code by url.
        /// </summary>
        ///<param name="url">Url of site.</param>
        /// <returns>Html code of url.</returns>
        public string GetHtmlCodeByUrl(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader str = new StreamReader(response.GetResponseStream());
                string html = str.ReadToEnd();
                return html;
            }
            catch (WebException ex)
            {
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                if (exResponse != null)
                {
                    switch (exResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound: break;
                        case HttpStatusCode.Forbidden: break;
                        default: break;
                    }
                }
                return null;
            }
        }
    }
}
