using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.DomainService.Interface
{
    public interface ISightingsDomainService
    {
        Task<int> AddSightingsReport(int pokemonId, int areaId, string location, DateTime reportTime);
        Task<List<SightingsReport>> GetSightingReports();
        Task<List<Area>>  GetAreas();
        Task<List<Pokemon>> GetPokemons();
        Task<int> AddNestReport(int pokemonid, int areaId, string location);
        Task<List<NestReport>> GetNestReports();
        Task<bool> AddOrUpdateDevice(string deviceId, string reg_id);
    }
}