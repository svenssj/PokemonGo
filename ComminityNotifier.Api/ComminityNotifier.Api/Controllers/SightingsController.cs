using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Core.ApplicationService;

namespace ComminityNotifier.Api.Controllers
{
    public class SightingsController : ApiController
    {
        private readonly ISightnigsApplicationService _appService;
        public SightingsController(ISightnigsApplicationService applicationService)
        {
            _appService = applicationService;
        }

        [HttpPost]
        [Route("AddSighting")]
        public async Task<ReportSightingsResponseObject> RepostSightning(int pokemonNumber, string area, string location)
        {

            var valid = ValidateSightingsReport(pokemonNumber, area, location).IsValid;
            if(valid)
            try
            {
                var response = await _appService.ReportSighting(pokemonNumber, area, location, DateTime.UtcNow);
                
                    return new ReportSightingsResponseObject();
                
            }
            catch (Exception e)
            {
                return new ReportSightingsResponseObject(e.Message);

            }
            return new ReportSightingsResponseObject
            {
                ErrorMessage = "An error Has occured"
            };
            
        }
        [HttpGet]
        [Route("GetSightings")]
        public async Task<List<GetSightingsResponseObject>> GetSightings()
        {
            var sightings = await _appService.GetReports();

            return sightings.Select(sightingsReport => new GetSightingsResponseObject
            {
                Pokemon = sightingsReport.Pokemon.ToString(), Area = sightingsReport.Area.ToString(), Location = sightingsReport.Locaiton, Time = sightingsReport.ReportTime.ToUniversalTime().ToString()
            }).ToList();

        }
        [HttpGet]
        [Route("GetValidAreas")]
        public async Task<List<string>> GetValidAreas()
        {
            return await _appService.GetAreas();

        }

        private ReportSightingsResponseObject ValidateSightingsReport(int pokemonNumber, string area, string location)
        {

            if (pokemonNumber < 0 || pokemonNumber > 151)
                return new ReportSightingsResponseObject("Pokemon is Invalid");
            if (string.IsNullOrWhiteSpace(area))
                return new ReportSightingsResponseObject("Area cannot be empty");
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
