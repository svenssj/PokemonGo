using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CommunityNotifier.Core.ApplicationService;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;


namespace ComminityNotifier.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var unityContainer = new UnityContainer();
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterTypes(unityContainer);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
            GlobalConfiguration.Configuration.DependencyResolver =
                new Unity.WebApi.UnityDependencyResolver(unityContainer);
        }
    }


}
