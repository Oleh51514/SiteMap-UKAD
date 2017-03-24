using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sitemap.API.Startup))]
namespace Sitemap.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
