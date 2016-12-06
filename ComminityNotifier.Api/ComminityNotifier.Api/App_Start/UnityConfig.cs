using Microsoft.Practices.Unity;
using System.Web.Http;
using CommunityNotifier.Core.ApplicationService;
using CommunityNotifier.Core.Domain.DomainService;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.Repository;
using Unity.WebApi;

namespace ComminityNotifier.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(UnityContainer container)
        {
		
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISightnigsApplicationService, SightnigsApplicationService>();
            container.RegisterType<ISightingsDomainService, SightingsDomainService>();
            container.RegisterType<ISightingsRepository, SightingsRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}