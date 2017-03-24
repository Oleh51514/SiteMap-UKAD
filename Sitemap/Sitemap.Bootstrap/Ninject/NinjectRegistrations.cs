using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Sitemap.BLL.Abstracts.Services;
using Sitemap.BLL.Services;
using Sitemap.Bootstrap.AutoMapper;
using Sitemap.Common;
using Sitemap.DAL;
using Sitemap.DAL.Abstracts.Repositories;
using Sitemap.DAL.Repositories;
using System;

namespace Sitemap.Bootstrap.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            InitializeAutomapper();
            InitializeServices();
            InitializeRepositories();
        }

        private void InitializeRepositories()
        {
            Bind<SitemapDbContext>().ToSelf().InRequestScope().WithConstructorArgument("ConnectionString", "DefaultConnection");
            Bind<IRepository>().To<Repository>().InRequestScope();
        }

        private void InitializeServices()
        {
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IDomainService>().To<DomainService>();
            Bind<INodeUrlService>().To<NodeUrlService>();
            Bind<ISiteMapService>().To<SiteMapService>();
            Bind<IUrlsParse>().To<UrlsParse>();
        }

        private void InitializeAutomapper()
        {
            Bind<MapperConfiguration>()
              .ToSelf()
              .InRequestScope()
              .WithConstructorArgument<Action<IMapperConfiguration>>(
                    cfg => new AutoMapperConfig(cfg));
            Bind<IConfigurationProvider>().ToMethod(ctx => ctx.Kernel.Get<MapperConfiguration>());
            Bind<IMapper>().ToMethod(maper => Kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
            Bind<IExpressionBuilder>().ToConstructor(ctx => new ExpressionBuilder(Kernel.Get<MapperConfiguration>()));
        }
    }
}
