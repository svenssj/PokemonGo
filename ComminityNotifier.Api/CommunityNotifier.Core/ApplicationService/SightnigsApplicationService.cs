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

        public async Task<int> ReportSighting(int pokemonId, int areaId, string location, DateTime reportTime)
        {
            
            return await _sightingsDomainService.AddSightingsReport(pokemonId,areaId,location,reportTime);
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

        public async Task<bool> AddRegistrationId(string deviceId, string registrationId)
        {
            return await _sightingsDomainService.AddRegistrationId(deviceId,registrationId);
        }
    }
}
