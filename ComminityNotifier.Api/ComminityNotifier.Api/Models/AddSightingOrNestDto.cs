namespace CommunityNotifier.Api.Models
{
    public class AddSightingOrNestDto
    {
        public int PokemonNumber { get; set; }
        public int AreaId { get; set; }
        public string Location { get; set; }
    }
}