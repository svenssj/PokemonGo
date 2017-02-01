using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace CommunityNotifier.Api
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
