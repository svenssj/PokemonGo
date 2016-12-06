using System;
using System.Collections.Generic;
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

        public async Task<int> ReportSighting(int pokemonId, string area, string location, DateTime reportTime)
        {
            return await _sightingsDomainService.AddSightingsReport(SightingsMapper.MapToEntity(pokemonId, area, location, reportTime));
        }

        public async Task<List<SightingsReport>> GetReports()
        {
            return await _sightingsDomainService.GetSightingReports();
        }

        public async Task<List<string>> GetAreas()
        {
            var list = new List<string>();
            list.Add(AreaEnum.Bjurhovda.ToString());
            list.Add(AreaEnum.Centrum.ToString());
            list.Add(AreaEnum.Djakneberget.ToString());
            list.Add(AreaEnum.Haga.ToString());
            list.Add(AreaEnum.Hemdal.ToString());
            list.Add(AreaEnum.Malmaberg.ToString());
            list.Add(AreaEnum.Oxbacken.ToString());
            list.Add(AreaEnum.Skiljebo.ToString());
            list.Add(AreaEnum.Viksang.ToString());

            return await Task.FromResult(list);
        }
    }
}
