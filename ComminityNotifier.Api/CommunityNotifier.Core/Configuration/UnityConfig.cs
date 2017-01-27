using CommunityNotifier.Core.ApplicationService;
using CommunityNotifier.Core.Domain.DomainService;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.ExternalService;
using CommunityNotifier.Core.Domain.Repository;
using Microsoft.Practices.Unity;

namespace CommunityNotifier.Core.Configuration
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<ISightnigsApplicationService, SightnigsApplicationService>();
            container.RegisterType<ISightingsDomainService, SightingsDomainService>();
            container.RegisterType<IRepository, SightingsRepository>();
            container.RegisterType<IFirebaseService, FirebaseService>();
            container.RegisterType<IFireBaseClient, FireBaseClient>();
        }
    }
}