using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Sightings")]
    public class SightingsController : ApiController
    {
        private readonly ISightnigsApplicationService _appService;
        public SightingsController(ISightnigsApplicationService applicationService)
        {
            _appService = applicationService;
        }

        [HttpPost]
        [Route("SetNotificationFilter")]
        public async Task<bool>
        AddOrUpdateNotificationFilter(SetNotificationFilterDTO notificationFilterDTO)
        {
            if (notificationFilterDTO.DeviceId.IsNullOrWhiteSpace())
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (!notificationFilterDTO.PokemonIds.Any() || !notificationFilterDTO.AreaIds.Any())
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return await _appService.AddOrUpdateNotificationFilter(notificationFilterDTO.DeviceId, notificationFilterDTO.PokemonIds, notificationFilterDTO.AreaIds);
        }
        public class SetNotificationFilterDTO
        {
          public  string DeviceId { get; set; }
            public List<int> PokemonIds { get; set; }

            public List<int> AreaIds { get; set; }
        }

        [HttpPost]
        [Route("AddSighting")]
     
        public async Task<ReportSightingsResponseObject> ReportSightning(int pokemonNumber, int area, string location)
        {

            var valid = ValidateSightingsReport(pokemonNumber, location).IsValid;
            if (!valid)
                return new ReportSightingsResponseObject
                {
                    ErrorMessage = "An error Has occured"
                };
            try
            {
                await _appService.ReportSighting(pokemonNumber, area, location, DateTime.UtcNow);

                return new ReportSightingsResponseObject();
            }
            catch (Exception e)
            {
                return new ReportSightingsResponseObject(e.Message);

            }

        }
        [HttpGet]
        [Route("GetSightings")]
        public async Task<List<GetSightingsResponseObject>> GetSightings()
        {
            var sightings = await _appService.GetReports();

            return sightings.Select(sightingsReport => new GetSightingsResponseObject
            {
                Pokemon = sightingsReport.Pokemon.PokemonName,
                Area = sightingsReport.Area.AreaName,
                Location = sightingsReport.Locaiton,
                Time = sightingsReport.ReportTime.ToString(CultureInfo.InvariantCulture) + "Z"
            }).ToList();

        }
        

       

         
    

        private ReportSightingsResponseObject ValidateSightingsReport(int pokemonNumber, string location)
        {

            if (pokemonNumber < 0 || pokemonNumber > 151)
                return new ReportSightingsResponseObject("Pokemon is Invalid");
            if (string.IsNullOrWhiteSpace(location))
                return new ReportSightingsResponseObject("Location cannot be empty");

            return new ReportSightingsResponseObject();

        }
    }

    public class GetSightingsResponseObject
    {
        public string Time { get; set; }
        public string Pokemon { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
    }

    public class ReportSightingsResponseObject
    {
        public ReportSightingsResponseObject()
        {
            ResponseKey = Guid.NewGuid();
        }
        public ReportSightingsResponseObject(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ResponseKey = Guid.NewGuid();
        }
        public bool IsValid => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; set; }
        public Guid ResponseKey { get; private set; }
    }
}
