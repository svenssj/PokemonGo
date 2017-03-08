using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Api.Models;
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
        [Route("AddSighting")]
     
        public async Task<ReportSightingsResponseObject> ReportSightning(AddSightingOrNestDto sightingOrNest)
        {

            var valid = ValidateSightingsReport(sightingOrNest.PokemonNumber, sightingOrNest.Location,sightingOrNest.DeviceId).IsValid;
            if (!valid)
                return new ReportSightingsResponseObject
                {
                    ErrorMessage = "An error Has occured"
                };
            try
            {
                await _appService.ReportSighting(sightingOrNest.PokemonNumber, sightingOrNest.AreaId, sightingOrNest.Location,sightingOrNest.DeviceId , DateTime.UtcNow);

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
        

         
    

        private ReportSightingsResponseObject ValidateSightingsReport(int pokemonNumber, string location,string deviceId)
        {

            if (pokemonNumber < 0 || pokemonNumber > 251)
                return new ReportSightingsResponseObject("Pokemon is invalid");
            if (string.IsNullOrWhiteSpace(location))
                return new ReportSightingsResponseObject("Location cannot be empty");
            if(string.IsNullOrWhiteSpace(deviceId))
                return new ReportSightingsResponseObject("DeviceId cannot be empty");

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
