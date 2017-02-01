using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class DeviceAreaFilter
    {
        [Key]
        public int Key { get; set; }
        public Device Device { get; set; }
        public Area Area { get; set; }
    }
}