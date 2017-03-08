using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.ApplicationService
{
    public interface ISightnigsApplicationService
    {
        Task<int> ReportSighting(int pokemonId, int areaId, string location, string deviceId, DateTime reportTime);
        Task<List<SightingsReport>> GetReports();
        Task<List<Area>> GetAreas();
        Task<List<Pokemon>>  GetPokemons();
        Task<List<NestReport>>  GetNestReports();
        Task<int> AddNestReport(int pokemonid, int areaid, string spot);
        Task<bool> AddOrUpdateDevice(string deviceId,string registrationId);


        Task<List<Pokemon>>  GetUserPokemonFilter(string deviceId);
        Task<List<Area>> GetUserAreaFilter(string deviceId);
        Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds);
    }
}