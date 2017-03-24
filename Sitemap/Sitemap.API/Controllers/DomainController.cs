using Sitemap.BLL.Abstracts.Services;
using Sitemap.Common;
using Sitemap.Common.DTO;
using System;
using System.Web.Mvc;

namespace Sitemap.API.Controllers
{
    public class DomainController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDomainService _domainService;
        private readonly ISiteMapService _siteMapService;

        /// <summary>
        /// Constructor where introduce dependence.
        /// </summary>
        public DomainController(IEmployeeRepository employeeRepository, IDomainService domainService, ISiteMapService siteMapService)
        {
            this._siteMapService = siteMapService;
            this._domainService = domainService;
            this._EmployeeRepository = employeeRepository;
        }
        // GET: Domain

        /// <summary>
        /// Main page with textbox, where user can enter url
        /// </summary>
        public ActionResult Index()
        {               
            return View();
        }

        /// <summary>
        /// Measurement site map
        /// </summary>
        /// <param name="domain">user enter domain url.</param>
        /// <returns>redirect to action MeasurementResult  </returns>
        [HttpPost]
        public ActionResult GetDomain(string domain)
        {
            string correctDomain = _domainService.GetCorrectfDomain(domain);
            var domainDto = new DomainDto
            {
                DomainUrl = correctDomain,
                MeasurementDate = DateTime.Now
            };
            _domainService.Add(domainDto);
            _siteMapService.DepthOfUrlsSearch = 2;
            _siteMapService.MeasurementSiteMap(correctDomain, domainDto.Id);
            return JavaScript("window.location = '" + Url.Action("Index", "MeasurementResult", new { idDomain = domainDto.Id }) + "'");

        }

    }
}