using System;
using System.Configuration;
using System.Threading.Tasks;
using RestSharp;

namespace CommunityNotifier.Core.Domain.ExternalService
{
    public interface IFireBaseClient
    {
        Task<bool> SendNotification(FireBaseNotification notification, string deviceId);
    }

   public class FireBaseClient : IFireBaseClient
    {

        private RestClient client;
        public FireBaseClient()
        {
            client = new RestClient("https://fcm.googleapis.com/fcm/send");
          
        }

     
        public async Task<bool> SendNotification(FireBaseNotification notification,string deviceId)
        {
            
            var key = "key=" + ConfigurationManager.AppSettings["FireBaseApiKey"];

            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", key);

            var body = "{\"to\" : \"" + deviceId + "\"," + "\"notification\": {\"body\": \"" + notification.Body + "\",    \"sound\": \"default\",    \"title\": \"" + notification.Header + "\" }     }";
            request.AddParameter("application/json", body, ParameterType.RequestBody);


            var result = client.Execute(request);
            return await Task.FromResult(true);
            // return result;
        }


        public class AndroidFCMPushNotificationStatus
        {
            public bool Successful
            {
                get;
                set;
            }

            public string Response
            {
                get;
                set;
            }
            public Exception Error
            {
                get;
                set;
            }
        }
    }
}