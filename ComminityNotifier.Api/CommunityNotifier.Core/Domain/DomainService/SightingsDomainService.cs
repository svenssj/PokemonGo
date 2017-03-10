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

        public async Task<bool> AddOrUpdateDevice(string deviceId,  string regId)
        {
         var response =  await _repository.RegisterOrUpdateDevice(deviceId, regId);
            var result = await _repository.SaveChangesAsync();
            if (response == RegisterOrUpdateResponseEnum.Registered)
            {
                //Adds all pokemons and areas to user preferences since this is the first start.
                await AddOrUpdateNotificationFilter(deviceId,
                    (await GetPokemons()).Select(p => p.PokemonNumber).ToList(),
                    (await GetAreas()).Select(a => a.AreaId).ToList());

             await   _firebaseService.SendNotification(new FireBaseNotification { Header = "Registrerad", Body = "Enheten är nu redo att ta emot notifieringar" },regId);
            }
          
            return result > 0;
        }

        public async Task<int> AddSightingsReport(int pokemonId, int areaId, string location, string deviceId, DateTime reportTime)
        {

            var reportingDevice = await _repository.GetDeviceById(deviceId);
            if (reportingDevice == null)
                throw new KeyNotFoundException("Enheten: "+deviceId + " kunde inte hittas");
            if(reportingDevice.Disabled)
                throw new InvalidOperationException("Enheten "+deviceId + " nekades att slutföra operationen");
            var sighting = new SightingsReport
            {
                Area = (await GetAreas()).FirstOrDefault(a => a.AreaId == areaId),
                Pokemon = await GetPokemonByNumber(pokemonId),
                Locaiton = location,
                DeviceId = deviceId,
                ReportTime = reportTime
            };
            _repository.AddReport(sighting);
            var repoResponse = await _repository.SaveChangesAsync();

            if (repoResponse <= 0) return repoResponse;
            var devices = await _repository.GetDevicesWithAreaAndPokemon(areaId,pokemonId);
            foreach (var device in devices)
            {
                await _firebaseService.SendNotification(new FireBaseNotification()
                {
                    Body = sighting.Area.AreaName +" - "+ sighting.Locaiton,
                    Header = sighting.Pokemon.PokemonName + " - siktad!"
                }, device.RegistrationId);
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

        public async Task<int> AddNestReport(int pokemonid, int areaId, string spot)
        {
            
             await _repository.AddNestReport( pokemonid,  areaId,  spot);
            return await _repository.SaveChangesAsync();

        }

        public async Task<List<NestReport>> GetNestReports()
        {
            return await _repository.GetNestReportsAsync();
        }

        public async Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds)
        {
           var repoResult = await _repository.AddOrUpdateNotificationFilter(deviceId, pokemonIds, areaIds);
            if (!repoResult)
                return false;
            var saveChangesResult = await _repository.SaveChangesAsync();
            return saveChangesResult > 0;
        }

        public async Task<List<Pokemon>> GetUserPokemonFilter(string deviceId)
        {
            return await _repository.GetUserPokemonFilter(deviceId);
        }

        public async Task<List<Area>> GetUserAreaFilter(string deviceId)
        {
            return await _repository.GetUserAreaFilter(deviceId);
        }
    }
}
