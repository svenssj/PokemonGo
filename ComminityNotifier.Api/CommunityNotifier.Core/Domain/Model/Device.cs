using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommunityNotifier.Core.Domain.Model
{
    public class Device
    {
        [Key]
        public string DeviceId { get; set; }
        public string RegistrationId { get; set; }
    }
}
