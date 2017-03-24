using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.Common.DTO
{
    public class NodeUrlForGridDto : IComparable<NodeUrlForGridDto>
    {
        public int CompareTo(NodeUrlForGridDto p)
        {
            return this.DomainUrl.CompareTo(p.DomainUrl);
        }

        public string id { get; set; }

        public string DomainUrl { get; set; }

        public int MinRespTime { get; set; }

        public int MaxRespTime { get; set; }

        public int level { get; set; }

        public string parent { get; set; }

        public bool isLeaf { get; set; }

        public bool expanded { get; set; }

        public bool loaded { get; set; }

        public string icon { get; set; }
    }
}
