using System.Collections.Generic;
using ComminityNotifier.Api.Models;

namespace CommunityNotifier.Api.Models
{
    public class NestListViewModel
    {
        public List<NestReportDTO> NestReports { get; set; }
        public List<AreaDTO> Areas { get; set; }
        public List<PokemonDTO> Pokemons { get; set; }
    }
}