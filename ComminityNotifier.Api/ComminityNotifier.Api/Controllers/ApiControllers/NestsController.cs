using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Nests")]
    public class NestsController: ApiController
    {
        private readonly ISightnigsApplicationService _appService;

        public NestsController(ISightnigsApplicationService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("GetNests")]
        public async Task<List<NestReportDTO>> GetNests()
        {
            var nests = await _appService.GetNestReports();

            return nests.Select(n => new NestReportDTO()
            {
                Pokemon = new PokemonDTO() { Name = n.Pokemon.PokemonName, Number = n.Pokemon.PokemonNumber },
                Location = n.Locations.Select(l => new LocationDTO() { Area = l.Area.AreaName, Spot = l.Spot }).ToList()

            }).ToList();
        }

        [HttpPost]
        [Route("AddNest")]
        public async Task<bool> AddNest(int pokemonId, int areaId, string spot)
        {
            if (string.IsNullOrWhiteSpace(spot))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var result = await _appService.AddNestReport(pokemonId, areaId, spot);

            return result > 0;
        }
    }
}