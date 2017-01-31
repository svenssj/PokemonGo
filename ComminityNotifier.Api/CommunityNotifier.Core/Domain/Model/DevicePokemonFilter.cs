using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class DevicePokemonFilter
    {
        [Key]
        public int Key { get; set; }

        public Device Device { get; set; }
        public Pokemon Pokemon { get; set; }    
    }
}