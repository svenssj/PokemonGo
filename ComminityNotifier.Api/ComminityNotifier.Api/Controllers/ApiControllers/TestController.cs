using System;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Api.Models;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    public class TestController : ApiController
    {
        private ReportSightingsResponseObject ValidateSightingsReport(int pokemonNumber, string location, string deviceId)
        {

            if (pokemonNumber < 0 || pokemonNumber > 251)
                return new ReportSightingsResponseObject("Pokemon is invalid");
            if (string.IsNullOrWhiteSpace(location))
                return new ReportSightingsResponseObject("Location cannot be empty");
            if (string.IsNullOrWhiteSpace(deviceId))
                return new ReportSightingsResponseObject("DeviceId cannot be empty");

            return new ReportSightingsResponseObject();

        }

        [HttpPost]
        [Route("AddSightingTEST")]

        public ReportSightingsResponseObject ReportSightningTest(AddSightingOrNestDto sightingOrNest)
        {

            var valid = ValidateSightingsReport(sightingOrNest.PokemonNumber, sightingOrNest.Location, sightingOrNest.DeviceId).IsValid;
            if (!valid)
                return new ReportSightingsResponseObject
                {
                    ErrorMessage = "An error Has occured"
                };
            try
            {

                return new ReportSightingsResponseObject();
            }
            catch (Exception e)
            {
                return new ReportSightingsResponseObject(e.Message);

            }

        }

        [HttpPost]
        [Route("AddNestTEST")]
        public async Task<bool> AddNestTEST(AddSightingOrNestDto nestSightingOrNest)
        {


            if (string.IsNullOrWhiteSpace(nestSightingOrNest.DeviceId))
                return false;
            if (string.IsNullOrWhiteSpace(nestSightingOrNest.Location))
                return false;
            if (nestSightingOrNest.PokemonNumber < 0 || nestSightingOrNest.PokemonNumber > 251)
                return false;
            if (nestSightingOrNest.AreaId == 0)
                return false;

            return true;
        }
    }
}