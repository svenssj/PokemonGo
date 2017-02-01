using System;
using System.Collections.Generic;
using ComminityNotifier.Api.Models;

namespace CommunityNotifier.Api.Models
{
    public class SightingsReportDTO
    {   
        public string PokemonName { get; set; }
        public AreaDTO Area { get; set; }
        public string Locaiton { get; set; }
        public DateTime ReportTime { get; set; }
    
    }

    public class NestReportDTO
    {
        public PokemonDTO Pokemon { get; set; }
        public List<LocationDTO> Location { get; set; }
    }

    public class LocationDTO
    {
        public string Area { get; set; }
        public string Spot { get; set; }
    }
}