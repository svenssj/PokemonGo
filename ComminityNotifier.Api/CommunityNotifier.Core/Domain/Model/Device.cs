using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class Device
    {
        [Key]
        public string DeviceId { get; set; }
        public string RegistrationId { get; set; }

        public bool Disabled { get; set; }
        public DateTime DisabledDate { get; set; }
    }
}
