using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.Model;
using CommunityNotifier.Core.Domain.Repository;

namespace CommunityNotifier.Core.Domain.DomainService
{
    internal class SightingsDomainService : ISightingsDomainService
    {
        private readonly IRepository _repository;
        public SightingsDomainService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddSightingsReport(int pokemonId, int areaId, string location, DateTime reportTime)
        {
            var sighting = new SightingsReport
            {
                Area = (await GetAreas()).FirstOrDefault(a => a.AreaId == areaId),
                Pokemon = await GetPokemonByNumber(pokemonId),
                Locaiton = location,
                ReportTime = reportTime
            };
            _repository.AddReport(sighting);
            return await _repository.SaveChangesAsync();
          }

        private async Task<Pokemon> GetPokemonByNumber(int pokemonNuber)
        {
            return await _repository.GetPokemonByNumber(pokemonNuber);
        }

        public async Task<List<SightingsReport>> GetSightingReports()
        {
            return await _repository.GetFreshReportsAsList();
        }

        public async Task<List<Area>> GetAreas()
        {
            return await _repository.GetAreasAsList();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _repository.GetPokemons();
        }
    }
}
