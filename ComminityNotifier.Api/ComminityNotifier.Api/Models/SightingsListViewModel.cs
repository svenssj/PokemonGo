using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommunityNotifier.Core.Domain.Model;

namespace ComminityNotifier.Api.Models
{
    public class SightingsListViewModel
    {
        public List<SightingsReportDTO> Sightings { get; set; }
        public List<AreaEnum> Areas { get; set; }
        public List<PokemonEnum> Pokemons { get; set; }
    }
}