using System;
using CommunityNotifier.Core.Domain;
using CommunityNotifier.Core.Domain.DomainService;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.ApplicationService
{
    public class SightingsMapper
    {
        public static SightingsReport MapToEntity(int pokemonId, string area, string location, DateTime reportTime)
        {
            return new SightingsReport
            {
                Area = AreaMapper.MapFromStringToEnum(area),
                Locaiton = location,
                Pokemon = (PokemonEnum)pokemonId,
                ReportTime = reportTime
            };
        }
    }
}