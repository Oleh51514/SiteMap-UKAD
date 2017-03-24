using Sitemap.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.DAL.Entities
{
    public class NodeUrl : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int MinRespTime { get; set; }
        public int MaxRespTime { get; set; }

        // foreign key and navigation property 
        public int DomainId { get; set; }
        public Domain Domain { get; set; }
    }
}
