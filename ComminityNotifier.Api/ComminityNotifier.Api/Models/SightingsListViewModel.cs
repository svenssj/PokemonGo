using System.Collections.Generic;
using ComminityNotifier.Api.Controllers;
using CommunityNotifier.Core.Domain.Model;

namespace ComminityNotifier.Api.Models
{
    public class SightingsListViewModel
    {
        public List<SightingsReportDTO> Sightings { get; set; }
        public List<AreaDTO> Areas { get; set; }
        public List<PokemonDTO> Pokemons { get; set; }
    }

    public class NestListViewModel
    {
        public List<NestReportDTO> NestReports { get; set; }
        public List<AreaDTO> Areas { get; set; }
        public List<PokemonDTO> Pokemons { get; set; }
    }
}