using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common.DTO;
using System.Web.Mvc;

namespace Sitemap.API.Controllers
{
    public class MeasurementResultController : Controller
    {
        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        ISiteMapService _siteMapService;
        private readonly INodeUrlService _nodeUrlService;
        public MeasurementResultController(INodeUrlService nodeUrlService, ISiteMapService siteMapService)
        {
            _nodeUrlService = nodeUrlService;
            _siteMapService = siteMapService;
        }

        /// <summary>
        /// Main page with charth and treeGrid
        /// </summary>
        public ActionResult Index(int idDomain)
        {
            return View();
        }

        /// <summary>
        /// Gets data for ajax charth. (nodeUrls list acc. to documentation).
        /// </summary>
        ///<param name="idDomain">ID of get domain.</param>
        /// <returns>NodeUrlDto list in json format.</returns>
        public JsonResult LoadUrlsForCharth(int idDomain)
        {
            var listNodes = _nodeUrlService.GetAllById(idDomain);
            return Json(listNodes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets data for ajax treeGrid. (nodeUrls list acc. to documentation).
        /// </summary>
        ///<param name="idDomain">ID of get domain.</param>
        /// <returns>NodeUrlForGridDto list in json format.</returns>
        public JsonResult GetDataForGrid(int idDomain)
        {
            var listNodes = _nodeUrlService.GetAllById(idDomain);
            NodeUrlForGridDto[] siteMap = _siteMapService.GenerateSitemap(listNodes);
            return Json(siteMap, JsonRequestBehavior.AllowGet);
        }       
    }
}