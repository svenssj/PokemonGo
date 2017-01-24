using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.ExternalService;
using CommunityNotifier.Core.Domain.Model;
using CommunityNotifier.Core.Domain.Repository;

namespace CommunityNotifier.Core.Domain.DomainService
{
    internal class SightingsDomainService : ISightingsDomainService
    {
        private readonly IRepository _repository;
        private readonly IFirebaseService _firebaseService;
        public SightingsDomainService(IRepository repository, IFirebaseService firebaseService)
        {
            _repository = repository;
            _firebaseService = firebaseService;
        }

        public async Task<bool> AddRegistrationId(string deviceId,  string reg_id)
        {
            var result =  await Task.FromResult(_repository.AddRegistrationId(deviceId, reg_id));
            return result != null;
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
            var repoResponse = await _repository.SaveChangesAsync();

            if (repoResponse > 0)
            {

                var devices = await _repository.GetDevices();
                foreach (var device in devices)
                {
                    var fbResponse = _firebaseService.SendNotification(new FireBaseNotification()
                    {
                        Body = sighting.Area.AreaName + " - " + sighting.Locaiton,
                        Header = sighting.Pokemon.PokemonName + " - " + sighting.Area.AreaName
                    }, device.RegistrationId);
                }
     
            }
            return repoResponse;
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
