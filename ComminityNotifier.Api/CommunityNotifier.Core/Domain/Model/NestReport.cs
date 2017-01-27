using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class NestReport
    {
        [Key]
        public Guid Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public List<Location> Locations { get; set; }
    }
}