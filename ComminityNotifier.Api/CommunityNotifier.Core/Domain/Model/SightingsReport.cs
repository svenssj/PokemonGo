using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class SightingsReport 
    {
        [Key]
        public Guid SightingsId { get; set; }
        public PokemonEnum Pokemon { get; set; }
        public AreaEnum Area { get; set; }
        public string Locaiton { get; set; }
        public DateTime ReportTime { get; set; }
    }
}