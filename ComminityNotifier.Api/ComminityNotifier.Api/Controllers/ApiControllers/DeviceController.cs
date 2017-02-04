using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using CommunityNotifier.Core.ApplicationService;
using Microsoft.Ajax.Utilities;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Devices")]
    public class DeviceController : ApiController
    {
        private readonly ISightnigsApplicationService _appService;

        public DeviceController(ISightnigsApplicationService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("AddOrUpdateDevice")]
        public async Task<bool> RegisterOrUpdateDevice(string deviceId, string registrationId)
        {
            if (string.IsNullOrWhiteSpace(registrationId) || string.IsNullOrWhiteSpace(deviceId))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return await _appService.AddOrUpdateDevice(deviceId, registrationId);
        }

        [HttpPost]
        [Route("SetDeviceNotificationPreferences")]
        public async Task<string>AddOrUpdateNotificationFilter(SetNotificationFilterDTO notificationFilterDTO)
        {
            if (notificationFilterDTO.DeviceId.IsNullOrWhiteSpace())
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (!notificationFilterDTO.PokemonIds.Any() || !notificationFilterDTO.AreaIds.Any())
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            return new JavaScriptSerializer().Serialize((await _appService.AddOrUpdateNotificationFilter(notificationFilterDTO.DeviceId, notificationFilterDTO.PokemonIds, notificationFilterDTO.AreaIds)));
        }
        public class SetNotificationFilterDTO
        {
            public string DeviceId { get; set; }
            public List<int> PokemonIds { get; set; }

            public List<int> AreaIds { get; set; }
        }
        [Route("GetDeviceNotificationPreferences")]
        [HttpGet]
        public async Task<PreferencesDto> GetUserPreferences(string deviceId)
        {
            return new PreferencesDto
            {
                UserPokemons = (await _appService.GetUserPokemonFilter(deviceId)).Select(p => p.PokemonNumber).ToList(),
                UserAreas = (await _appService.GetUserAreaFilter(deviceId)).Select(a => a.AreaId).ToList()
            };

        }
    }

    public class PreferencesDto
    {
        public List<int> UserPokemons { get; set; }
        public List<int> UserAreas { get; set; }
    }
}