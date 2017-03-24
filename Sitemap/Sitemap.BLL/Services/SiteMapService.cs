using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common.DTO;
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
    public class SiteMapService : ISiteMapService
    {
        private bool _isServerResponseCodeOk;
        public int DepthOfUrlsSearch { get; set; }
        private readonly INodeUrlService _nodeUrlService;
        private readonly IUrlsParse _urlsParse;

        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        public SiteMapService(INodeUrlService nodeUrlService, IUrlsParse urlsParse)
        {
            _nodeUrlService = nodeUrlService;
            _urlsParse = urlsParse;
        }

        /// <summary>
        /// Measurement site map.
        /// </summary>
        /// <param name="domain">User enter domain url.</param>
        /// <param name="IdDomain">ID of get domain.</param>
        public void MeasurementSiteMap(string domain, int IdDomain)
        {
            List<String> listUrl = _urlsParse.GetListUrlsOfSiteMap(domain);
            CalculateSpeedOfResponse(listUrl, IdDomain);
        }

        /// <summary>
        /// Calculate s peed of response.
        /// </summary>
        ///<param name="listUrl">List node urls site.</param>
        ///<param name="IdDomain">ID domain.</param>
        public void CalculateSpeedOfResponse(List<string> listUrl, int IdDomain)
        {
            foreach (var x in listUrl)
            {
                var responseTime = new List<int>();
                _isServerResponseCodeOk = true;
                for (int i = 0; i < DepthOfUrlsSearch; i++)
                {
                    responseTime.Add(GetResponseTime(x));
                }
                if (_isServerResponseCodeOk)
                {
                    var nodeUrlDto = new NodeUrlDto
                    {
                        DomainId = IdDomain,
                        MaxRespTime = responseTime.Max(),
                        MinRespTime = responseTime.Min(),
                        Url = x
                    };
                    _nodeUrlService.Add(nodeUrlDto);
                }
            }
        }

        /// <summary>
        /// Get time of response.
        /// </summary>
        ///<param name="url">Url of site.</param>
        /// <returns>Time in seconds.</returns>
        public int GetResponseTime(string url)
        {
            if (GetServerResponseStatusCode(url) != (int)HttpStatusCode.OK)
            {
                _isServerResponseCodeOk = false;
                return 0;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
            timer.Stop();
            Thread.Sleep(100);
            return timer.Elapsed.Milliseconds;
        }

        /// <summary>
        /// Get server response status code.
        /// </summary>
        ///<param name="currentPage">Current url of site.</param>
        /// <returns>Status code.</returns>
        private int GetServerResponseStatusCode(string currentPage)
        {            
            HttpWebResponse response = null;
            int statusCode = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(currentPage);
                response = (HttpWebResponse)request.GetResponse();
                statusCode = (int)response.StatusCode;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    statusCode = (int)response.StatusCode;
                }
            }
            finally
            {
                if (response != null)
                { response.Close(); }
            }
            return statusCode;
        }

        /// <summary>
        /// Get tree grid of site map.
        /// </summary>
        ///<param name="domains">List node urls site.</param>
        /// <returns>Tree grid stucture NodeUrlForGridDto list.</returns>
        public NodeUrlForGridDto[] GenerateSitemap(IEnumerable<NodeUrlDto> domains)
        {
            HashSet<NodeUrlForGridDto> rooteDomains = new HashSet<NodeUrlForGridDto>();
            List<NodeUrlForGridDto> childsDomains = new List<NodeUrlForGridDto>();
            int n = 0;
            foreach (var item1 in domains)
            {
                int depthOfUrl1 = GetDepthOfUrl(item1.Url);
                IEnumerable<NodeUrlDto> itemdomains = domains;
                foreach (var item2 in itemdomains)
                {
                    int depthOfUrl2 = GetDepthOfUrl(item2.Url);
                    if (item2.Url.StartsWith(item1.Url) && !childsDomains.Exists(x => x.DomainUrl == item2.Url) && item2.Url != item1.Url && (depthOfUrl1 + 1) <= depthOfUrl2)
                    {
                        if (n == 0 && !childsDomains.Exists(x => x.DomainUrl == item1.Url))
                        {
                            rooteDomains.Add(new NodeUrlForGridDto
                            {
                                id = item1.Id.ToString(),
                                DomainUrl = item1.Url,
                                MaxRespTime = item1.MaxRespTime,
                                MinRespTime = item1.MinRespTime,
                                level = 0,
                                parent = null,
                                isLeaf = true,
                                expanded = true,
                                loaded = true,
                                icon = "ui-icon-link"
                            });
                            n++;
                        }
                        childsDomains.Add(new NodeUrlForGridDto
                        {
                            id = item2.Id.ToString(),
                            DomainUrl = item2.Url,
                            MaxRespTime = item2.MaxRespTime,
                            MinRespTime = item2.MinRespTime,
                            level = depthOfUrl2 - 1,
                            parent = item1.Id.ToString(),
                            isLeaf = true,
                            expanded = true,
                            loaded = true,
                            icon = "ui-icon-link"
                        });
                    }
                }
                if (n == 0)
                {
                    if (!childsDomains.Exists(x => x.DomainUrl == item1.Url))
                    {
                        rooteDomains.Add(new NodeUrlForGridDto
                        {
                            id = item1.Id.ToString(),
                            DomainUrl = item1.Url,
                            MaxRespTime = item1.MaxRespTime,
                            MinRespTime = item1.MinRespTime,
                            level = 0,
                            parent = null,
                            isLeaf = true,
                            expanded = true,
                            loaded = true,
                            icon = "ui-icon-link"
                        });
                    }
                }
            }
            rooteDomains.UnionWith(childsDomains);
            rooteDomains.OrderBy(x => x.DomainUrl).ToArray();
            NodeUrlForGridDto[] SortList = rooteDomains.ToArray();
            Array.Sort(SortList);
            return SortList;
        }

        /// <summary>
        /// Get depth of url.
        /// </summary>
        ///<param name="url">Url of site.</param>
        /// <returns>Count depth.</returns>
        public int GetDepthOfUrl(string url)
        {
            Uri uri = new Uri(url);
            string _urlWithoutScheme1 = url.Remove(0, uri.Scheme.Count() + 3);
            string[] _arr = _urlWithoutScheme1.Split('/');
            return _arr.Count();
        }
    }
}
