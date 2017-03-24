using Sitemap.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.DAL
{
    public class SitemapDbContext: DbContext
    {
        public SitemapDbContext()
            :base("DbConnection")
        {

        }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<NodeUrl> NodeUrls { get; set; }
    }
}
