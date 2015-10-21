using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using YooPoon.Core.Autofac;

namespace Protoss
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
//            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
//            RouteConfig.RegisterRoutes(RouteTable.Routes);            
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            #region IOC配置

            //DI配置
            var initialize = new InitializeContainer();
            initialize.Initializing();
            //WebAPI
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(initialize.ContainerManager.Container);

            //MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(initialize.ContainerManager.Container));
            
            #endregion


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}