using Sitemap.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.DAL.Entities
{
    public class Domain : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string DomainUrl { get; set; }
        public DateTime MeasurementDate { get; set; }

        // navigation property 
        public List<NodeUrl> NodeUrls { get; set; }

        public Domain()
        {
            NodeUrls = new List<NodeUrl>();
        }

    }
}
