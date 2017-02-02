using System.Collections.Generic;

namespace CommunityNotifier.Api.Models
{
    public class SightingsListViewModel2
    {
        public List<SightingsReportDTO> Sightings { get; set; }
        public List<AreaDTO> Areas { get; set; }
        public List<PokemonDTO> Pokemons { get; set; }
    }
}