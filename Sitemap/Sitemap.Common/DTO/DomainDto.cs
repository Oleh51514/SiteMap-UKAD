using Sitemap.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.Common.DTO
{
    public class DomainDto : IBaseDto<int>
    {
        public int Id { get; set; }
        public string DomainUrl { get; set; }
        public DateTime MeasurementDate { get; set; }
    }
}
