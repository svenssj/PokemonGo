using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    public class DeviceController : ApiController
    {
        private readonly ISightnigsApplicationService _appService;

        public DeviceController(ISightnigsApplicationService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [System.Web.Http.Route("AddOrUpdateDevice")]
        public async Task<bool> RegisterOrUpdateDevice(string deviceId, string registrationId)
        {
            if (string.IsNullOrWhiteSpace(registrationId) || string.IsNullOrWhiteSpace(deviceId))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return await _appService.AddOrUpdateDevice(deviceId, registrationId);
        }

        [HttpGet]
        public async Task<List<PreferencesDto>> GetUserPreferences(string deviceId)
        {
            return (await _appService.GetUserPreferences(deviceId)).Select
        }
    }

    public class PreferencesDto
    {
        public List<int> UserPokemons { get; set; }
        public List<int> UserAreas { get; set; }
    }
}