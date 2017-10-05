using System;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Framework.Core.Configuration;
using Framework.Core.Data;
using Framework.Core.Fakes;
using Framework.Core.Helper;
using Framework.Core.Infrastructure.DependencyManagement;
using Framework.Core.Infrastructure.TypeFinder;
using Framework.Data.Context;
using Framework.Data.Provider;
using Framework.Data.Repository;

namespace Framework.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, WebConfig config)
        {

            //Http
            builder.Register(p => HttpContext.Current == null
                ? (new FakeHttpContext("~/") as HttpContextBase)
                : (new HttpContextWrapper(HttpContext.Current) as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //Web Helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //Controller
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            var dataSettingsManager = new DataSettingsManager();
            var dataSettings = dataSettingsManager.LoadSettings();
            builder.Register(p => dataSettingsManager.LoadSettings())
                .As<DataSettings>();
            builder.Register(p => new EfDataProviderManager(dataSettings))
                .As<BaseDataProviderManager>();
            builder.Register(p => p.Resolve<BaseDataProviderManager>().LoadDataProvider())
                .As<IDataProvider>();

            if (dataSettings != null && dataSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettings);
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register(p => new EntityContext(dataSettings.DataConnectionString)).As<IDbContext>();
            }
            else
            {
                throw new ArgumentException("dataSettings is invalid");
            }

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

        }

        public int Order
        {
            get { return 0; }
        }

    }

}
