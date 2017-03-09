using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityNotifier.Core.Domain.Model
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public Area Area { get; set; }
        public string Spot { get; set; }
        public DateTime LocationTimeStamp { get; set; }
    }
}