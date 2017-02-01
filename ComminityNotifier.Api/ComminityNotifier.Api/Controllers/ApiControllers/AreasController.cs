using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ComminityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Areas")]
    public class AreasController : ApiController
    {
        private readonly ISightnigsApplicationService _sightingsApplicationService;

        public AreasController(ISightnigsApplicationService sightingsApplicationService)
        {
            _sightingsApplicationService = sightingsApplicationService;
        }

        [HttpGet]
        [Route("GetAreas")]
        public async Task<List<AreaDTO>> GetValidAreas()
        {
            var areas = await _sightingsApplicationService.GetAreas();

            return areas.Select(area => new AreaDTO
            {
                AreaName = area.AreaName,
                Id = area.AreaId
            }).ToList();

        }
    }
}