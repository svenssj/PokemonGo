using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityNotifier.Core.Domain.Model
{
    public class SightingsReport 
    {
        [Key]
        public Guid SightingsId { get; set; }
        [ForeignKey("Pokemon")]
        public int PokemonNumber { get; set; }
 
        public virtual Pokemon Pokemon { get; set; }
        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public string Locaiton { get; set; }
        public DateTime ReportTime { get; set; }
        public string DeviceId { get; set; }
        public int Iv { get; set; }
        public string FastMove { get; set; }
        public string ChargeMove { get; set; }
    }
}