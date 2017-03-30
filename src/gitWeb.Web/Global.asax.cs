using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Autofac.Integration.WebApi;
using gitWeb.Core.Features.Commit;
using LibGit2Sharp;
using gitWeb.Core;
using gitWeb.Core.Features.Configuration;
using gitWeb.Core.Features.Stage;

namespace gitWeb.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(Global).Assembly);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<WebConfig>().As<IConfigurationRepository>();
            builder.RegisterType<CommitProvider>().As<ICommitProvider>();
            builder.RegisterType<StagingAreaProvider>().As<IStagingAreaProvider>();
            builder.RegisterType<RepositoryFactory>().AsSelf();    
            builder.Register(c => c.Resolve<RepositoryFactory>().GetRepository()).SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}