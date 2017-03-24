using Sitemap.BLL.Abstracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitemap.API.Controllers
{
    public class ArhiveController : Controller
    {
        private readonly IDomainService _domainService;

        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        public ArhiveController( IDomainService domainService)
        {
            this._domainService = domainService;
        }
        // GET: Arhive

        /// <summary>
        /// Main page, that contains grid of domains.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets data for ajax grid. (domains list acc. to documentation).
        /// </summary>
        /// <returns>DomainDto list in json format.</returns>
        public JsonResult LoadDomains()
        {
            var listNodes = _domainService.GetAll();
            return Json(listNodes, JsonRequestBehavior.AllowGet);
        }
    }
}