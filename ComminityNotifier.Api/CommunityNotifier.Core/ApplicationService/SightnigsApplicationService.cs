using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.ApplicationService
{
    public class SightnigsApplicationService : ISightnigsApplicationService
    {

        private readonly ISightingsDomainService _sightingsDomainService;
        public SightnigsApplicationService(ISightingsDomainService sightingsDomainService)
        {
            _sightingsDomainService = sightingsDomainService;
        }

        public async Task<int> ReportSighting(int pokemonId, int areaId,string deviceId, string location, DateTime reportTime)
        {
            
            return await _sightingsDomainService.AddSightingsReport(pokemonId,areaId,location,deviceId,reportTime);
        }

        public async Task<List<SightingsReport>> GetReports()
        {
            return (await _sightingsDomainService.GetSightingReports()).OrderByDescending(x=>x.ReportTime).ToList();
        }

        public async Task<List<Area>> GetAreas()
        {
            return (await _sightingsDomainService.GetAreas()).OrderBy(a => a.AreaName).ToList();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _sightingsDomainService.GetPokemons();
        }

        public async Task<int> AddNestReport(int pokemonId, int areaId, string spot,string deviceId)
        {
            return await _sightingsDomainService.AddNestReport(pokemonId, areaId, spot,deviceId);
        }

        public async Task<List<NestReport>> GetNestReports()
        {
            return (await _sightingsDomainService.GetNestReports()).OrderBy(nr=>nr.Pokemon.PokemonNumber).ToList();
        }

        public async Task<bool> AddOrUpdateDevice(string deviceId, string registrationId)
        {
            return await _sightingsDomainService.AddOrUpdateDevice(deviceId,registrationId);
        }

        public async Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds)
        {
            return await _sightingsDomainService.AddOrUpdateNotificationFilter(deviceId, pokemonIds, areaIds);
        }

        public async Task<List<Pokemon>> GetUserPokemonFilter(string deviceId)
        {
            return await _sightingsDomainService.GetUserPokemonFilter(deviceId);
        }

        public async Task<List<Area>> GetUserAreaFilter(string deviceId)
        {
            return await _sightingsDomainService.GetUserAreaFilter(deviceId);
        }

   

        public  Task SetDisabledState(string deviceId, bool disabledState)
        {
            return  _sightingsDomainService.SetDisabledState(deviceId, disabledState);
        }
    }
}
