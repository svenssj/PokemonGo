using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityNotifier.Core.Domain.Model
{
    [Table("Areas")]
    public class Area
    {
        [Key]
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public virtual ICollection<SightingsReport> SightingsReport { get; set; }
    }


}