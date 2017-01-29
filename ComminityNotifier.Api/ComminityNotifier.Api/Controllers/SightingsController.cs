using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ComminityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;
using Microsoft.Ajax.Utilities;

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
        public async Task<ReportSightingsResponseObject> ReportSightning(int pokemonNumber, int area, string location)
        {

            var valid = ValidateSightingsReport(pokemonNumber,location).IsValid;
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
                Time = sightingsReport.ReportTime.ToString(CultureInfo.InvariantCulture)+"Z"
            }).ToList();

        }
        [HttpGet]
        [Route("GetValidAreas")]
        public async Task<List<AreaDTO>> GetValidAreas()
        {
            var areas = await _appService.GetAreas();

            var response = new List<AreaDTO>();
            foreach (var area in areas)
            {
                response.Add(new AreaDTO
                {
                    AreaName = area.AreaName,
                    Id = area.AreaId
                });
            }
            return response;

        }

        [HttpPost]
        [Route("AddOrUpdateDevice")]
        public async Task<bool> RegisterOrUpdateDevice(string deviceId ,string registrationId)
        {
            if(string.IsNullOrWhiteSpace(registrationId)||string.IsNullOrWhiteSpace(deviceId))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            return await _appService.AddOrUpdateDevice(deviceId,registrationId);
        }

         
        [HttpGet]
        [Route("GetNests")]
        public async Task<List<NestReportDTO>> GetNests()
        {
            var nests = await _appService.GetNestReports();

            return nests.Select(n => new NestReportDTO()
            {
                Pokemon = new PokemonDTO() {Name = n.Pokemon.PokemonName, Number = n.Pokemon.PokemonNumber},
                Location = n.Locations.Select(l => new LocationDTO() {Area = l.Area.AreaName, Spot = l.Spot}).ToList()

            }).ToList();
        }

        [HttpPost]
        [Route("AddNest")]
        public async Task<bool> AddNest(int pokemonId, int areaId, string spot)
        {
            if (spot.IsNullOrWhiteSpace())
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var result = await _appService.AddNestReport(pokemonId, areaId, spot);

            return result>0;
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
