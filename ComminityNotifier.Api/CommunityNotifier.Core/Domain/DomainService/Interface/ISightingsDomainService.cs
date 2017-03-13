using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.DomainService.Interface
{
    public interface ISightingsDomainService
    {
        Task<int> AddSightingsReport(int pokemonId, int areaId, string location,string deviceId, DateTime reportTime);
        Task<List<SightingsReport>> GetSightingReports();
        Task<List<Area>>  GetAreas();
        Task<List<Pokemon>> GetPokemons();
        Task<int> AddNestReport(int pokemonid, int areaId, string location,string deviceId);
        Task<List<NestReport>> GetNestReports();
        Task<bool> AddOrUpdateDevice(string deviceId, string reg_id);
        Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds);
        Task<List<Pokemon>> GetUserPokemonFilter(string deviceId);
        Task<List<Area>> GetUserAreaFilter(string deviceId);
        Task SetDisabledState(string deviceId,bool disabledState);
    }
}