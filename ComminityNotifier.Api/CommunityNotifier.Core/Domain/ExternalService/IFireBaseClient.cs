using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNotifier.Core.Domain.ExternalService
{
    public interface IFireBaseClient
    {
        Task<bool> SendNotification(FireBaseNotification notification, string deviceId);
    }

   public class FireBaseClient : IFireBaseClient
    {
        public async Task<bool> SendNotification(FireBaseNotification notification,string deviceId)
        {
            AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();

            try
            {
                result.Successful = false;
                result.Error = null;

                var value = notification.Header;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", ConfigurationManager.AppSettings["FireBaseApiKey"]));
                tRequest.Headers.Add(string.Format("Sender: id={0}", ConfigurationManager.AppSettings["FireBaseSenderId"]));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = await tReader.ReadToEndAsync();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return true;
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