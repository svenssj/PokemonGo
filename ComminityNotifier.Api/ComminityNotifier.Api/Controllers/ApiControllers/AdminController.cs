using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Admin")]
    public class AdminController : ApiController{ 

            private readonly ISightnigsApplicationService _sightingsApplicationService;

    public AdminController(ISightnigsApplicationService sightingsApplicationService)
    {
        _sightingsApplicationService = sightingsApplicationService;
    }
    
        [HttpPost]
        [Route("SetDeviceDisabledState")]
        // ReSharper disable once UnusedParameter.Global
        public async Task<bool> SetDeviceDisabledState(string deviceId,bool disabledState, string authToken)
        {

            if(authToken!=ConfigurationManager.AppSettings.Get("AdminAuth"))
                throw new UnauthorizedAccessException();
            try
            {
                await _sightingsApplicationService.SetDisabledState(deviceId,disabledState);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        [Route("RemoveNestsOlderThanDate")]
        public async Task<int> RemoveNestsBeforeDate(DateTime date,string authToken )
        {
            if (authToken != ConfigurationManager.AppSettings.Get("AdminAuth"))
                throw new UnauthorizedAccessException();
            try
            {
              return await  _sightingsApplicationService.RemoveNestsBeforeDate(date);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}