using System;

namespace ComminityNotifier.Api.Models
{
    public class SightingsReportDTO
    {   
        public string PokemonName { get; set; }
        public string Area { get; set; }
        public string Locaiton { get; set; }
        public DateTime ReportTime { get; set; }
    
    }
}