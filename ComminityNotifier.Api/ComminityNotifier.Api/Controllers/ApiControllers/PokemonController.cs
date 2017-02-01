using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CommunityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;

namespace CommunityNotifier.Api.Controllers.ApiControllers
{
    [RoutePrefix("Pokemons")]
    public class PokemonController : ApiController
    {
        private readonly ISightnigsApplicationService _sightnigsApplicationService;

        public PokemonController(ISightnigsApplicationService sightnigsApplicationService)
        {
            _sightnigsApplicationService = sightnigsApplicationService;
        }

        [HttpGet]
        [Route("GetPokemons")]
        public async Task<List<PokemonDTO>> GetPokemonsAsync()
        {
            var pokemons = await _sightnigsApplicationService.GetPokemons();
            return pokemons.Select(p => new PokemonDTO() {Name = p.PokemonName, Number = p.PokemonNumber}).ToList();
        }
    }
}