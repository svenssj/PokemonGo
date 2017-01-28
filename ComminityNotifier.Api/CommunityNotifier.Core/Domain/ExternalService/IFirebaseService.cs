using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNotifier.Core.Domain.ExternalService
{
    public interface IFirebaseService
    {
        Task<bool> SendNotification(FireBaseNotification notification, string firebaseId);
    }

    public class FirebaseService : IFirebaseService
    {

        private readonly IFireBaseClient _client;

        public FirebaseService(IFireBaseClient client)
        {
            _client = client;
        }
        public async Task<bool> SendNotification(FireBaseNotification notification, string firebaseId)
        {
          return  await _client.SendNotification(notification,  firebaseId);
        }
    }

    public class FireBaseNotification
    {
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
