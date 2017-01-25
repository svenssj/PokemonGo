using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class Pokemon
    {
        [Key]
        public int PokemonNumber { get; set; }
        public string PokemonName { get; set; }
        public virtual ICollection<SightingsReport>  SightingsReports { get; set; }
}
}