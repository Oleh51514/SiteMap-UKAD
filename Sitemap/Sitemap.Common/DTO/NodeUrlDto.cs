using Sitemap.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.Common.DTO
{
    public class NodeUrlDto : IBaseDto<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int MinRespTime { get; set; }
        public int MaxRespTime { get; set; }
        public int DomainId { get; set; }
    }
}
