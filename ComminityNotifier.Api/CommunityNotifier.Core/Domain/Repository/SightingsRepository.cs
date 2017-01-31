using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.Repository
{

    internal class SightingsRepository : IRepository
    {
        private readonly SightingsContext _sightingsContext;

        public SightingsRepository()
        {
            _sightingsContext = new SightingsContext();

        }

        public void Dispose()
        {
            _sightingsContext.Dispose();
        }

        public async Task<List<SightingsReport>> GetFreshReportsAsList()
        {
            var now = DateTime.UtcNow;


            IQueryable<SightingsReport> query = _sightingsContext.SightingsReports;
            query = query.Include("Area");
            query = query.Include("Pokemon");

            var list = await query.ToListAsync();
            var newList = new List<SightingsReport>();

            foreach (var sightingsReport in list)
            {

                var span = (now - sightingsReport.ReportTime).TotalMinutes;
                if (span < 60)
                    newList.Add(sightingsReport);
            }
            return newList;
        }

        public Guid AddReport(SightingsReport reportToAdd)
        {
            var guid = Guid.NewGuid();
            reportToAdd.SightingsId = guid;
            _sightingsContext.SightingsReports.Add(reportToAdd);

            return guid;
        }

        public void RemoveReport(Guid guid)
        {
            var sightingToRemove = _sightingsContext.SightingsReports.FirstOrDefault(rep => rep.SightingsId == guid);
            if (sightingToRemove != null)
                _sightingsContext.SightingsReports.Remove(sightingToRemove);

        }

        public void RemoveReport(SightingsReport reportToRemove)
        {
            RemoveReport(reportToRemove.SightingsId);
        }

        public void SaveChanges()
        {
            _sightingsContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _sightingsContext.SaveChangesAsync();
        }

        public async Task<List<Area>> GetAreasAsList()
        {
            return await _sightingsContext.Areas.ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByNumber(int pokemonNumber)
        {
            return (await GetPokemons()).FirstOrDefault(p => p.PokemonNumber == pokemonNumber);
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _sightingsContext.Pokemons.ToListAsync();
        }

        public async Task<Guid> AddNestReport(int pokemonid, int areaId, string spot)
        {
            var pokemon = await GetPokemonByNumber(pokemonid);
            if (pokemon == null)
                throw new IndexOutOfRangeException();
            var area = (await GetAreasAsList()).First(a => a.AreaId == areaId);
            if (area == null)
                throw new IndexOutOfRangeException();

            var report =
                (await GetNestReportsAsync()).FirstOrDefault(
                    rep => rep.Pokemon.PokemonNumber == pokemonid);
            //Add locaiton to existing nest
            if (report != null)
            {
                report.Locations.Add(new Location() { Area = area, LocationTimeStamp = DateTime.UtcNow, Spot = spot });
                return report.Id;
            }
            else
            {
                var guid = Guid.NewGuid();
                _sightingsContext.NestReports.Add(new NestReport()
                {
                    Id = guid,
                    Locations =
                        new List<Location>()
                        {
                            new Location() {Area = area, LocationTimeStamp = DateTime.UtcNow, Spot = spot}
                        },
                    Pokemon = pokemon
                });
                return guid;
            }

        }

        public async Task<List<NestReport>> GetNestReportsAsync()
        {

            IQueryable<NestReport> query = _sightingsContext.NestReports;
            query = query.Include("Locations");
            query = query.Include("Locations.Area");
            query = query.Include("Pokemon");
            return await query.ToListAsync();
        }
        private async Task<List<DeviceAreaFilter>> GetDeviceAreaFiltersAsync()
        {

            IQueryable<DeviceAreaFilter> query = _sightingsContext.DeviceAreaFilter;
            query = query.Include("Device");
            query = query.Include("Area");
            return await query.ToListAsync();
        }
        private async Task<List<DevicePokemonFilter>> GetDevicePokemonFilterAsync()
        {

            IQueryable<DevicePokemonFilter> query = _sightingsContext.DevicePokemonFilter;
            query = query.Include("Device");
            query = query.Include("Pokemon");
            return await query.ToListAsync();
        }

        public async Task<RegisterOrUpdateResponseEnum> RegisterOrUpdateDevice(string deviceId, string regId)
        {
            try
            {
                var existingDevice = await GetExistingDeviceOrNull(deviceId);

                //Add new registration
                if (existingDevice == null)
                {
                    _sightingsContext.Devices.Add(new Device() { DeviceId = deviceId, RegistrationId = regId });
                    return RegisterOrUpdateResponseEnum.Registered;
                }
                //Update existing
                else
                {
                    existingDevice.RegistrationId = regId;
                    return RegisterOrUpdateResponseEnum.Updated;
                }

            }
            catch (Exception)
            {
                return RegisterOrUpdateResponseEnum.Error;
            }

        }

        private async Task<Device> GetExistingDeviceOrNull(string deviceId)
        {
            return await _sightingsContext.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
        }

        public async Task<List<Device>> GetDevices()
        {
            return await _sightingsContext.Devices.ToListAsync();
        }

        public async Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds)
        {
            var pokemons = await GetPokemons();
            var areas = await GetAreasAsList();

            var device = await GetDeviceById(deviceId);

            foreach (var currentDevice in (await GetDeviceAreaFiltersAsync()).Where(daf => daf.Device.DeviceId == deviceId))
            {
                _sightingsContext.DeviceAreaFilter.Remove(currentDevice);
            }
            foreach (var currentDevice in (await GetDevicePokemonFilterAsync()).Where(daf => daf.Device.DeviceId == deviceId))
            {
                _sightingsContext.DevicePokemonFilter.Remove(currentDevice);
            }


            foreach (var areaId in areaIds)
            {
                _sightingsContext.DeviceAreaFilter.Add(new DeviceAreaFilter()
                {
                    Device = device,
                    Area = areas.FirstOrDefault(a => a.AreaId == areaId)
                });
            }
            foreach (var pokemonId in pokemonIds)
            {
                _sightingsContext.DevicePokemonFilter.Add(new DevicePokemonFilter()
                {
                    Device = device,
                    Pokemon = pokemons.FirstOrDefault(p => p.PokemonNumber == pokemonId)
                });
            }



            return true;
        }

        private async Task<Device> GetDeviceById(string deviceId)
        {
            return await _sightingsContext.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
        }

        public async Task<List<Device>> GetDevicesWithAreaAndPokemon(int areaId, int pokemonId)
        {
            var areaDevices = (await GetDeviceAreaFiltersAsync()).Where(daf => daf.Area.AreaId == areaId).Select(daf=>daf.Device);
            var pokemonDevices =
                (await GetDevicePokemonFilterAsync()).Where(dpf => dpf.Pokemon.PokemonNumber == pokemonId).Select(dpf=>dpf.Device);
            return areaDevices.Where(ad => pokemonDevices.Any(pd => pd.DeviceId == ad.DeviceId)).ToList();
        }
    }

    internal interface IRepository : IDisposable
    {
        Task<List<SightingsReport>> GetFreshReportsAsList();
        Guid AddReport(SightingsReport reportToAdd);
        void RemoveReport(Guid guid);
        void RemoveReport(SightingsReport reportToRemove);
        void SaveChanges();

        Task<int> SaveChangesAsync();

        Task<List<Area>> GetAreasAsList();
        Task<Pokemon> GetPokemonByNumber(int pokemonNumber);
        Task<List<Pokemon>> GetPokemons();

        Task<Guid> AddNestReport(int pokemonid, int areaId, string spot);
        Task<List<NestReport>> GetNestReportsAsync();
        Task<RegisterOrUpdateResponseEnum> RegisterOrUpdateDevice(string deviceId, string regId);
        Task<List<Device>> GetDevices();
        Task<bool> AddOrUpdateNotificationFilter(string deviceId, List<int> pokemonIds, List<int> areaIds);
        Task<List<Device>> GetDevicesWithAreaAndPokemon(int areaId, int pokemonId);
    }

    public class SightingsContext : DbContext
    {

        public SightingsContext()
        {
            Database.SetInitializer<SightingsContext>(null);
        }
        public IDbSet<SightingsReport> SightingsReports { get; set; }
        public IDbSet<Area> Areas { get; set; }
        public IDbSet<Pokemon> Pokemons { get; set; }
        public IDbSet<NestReport> NestReports { get; set; }
        public IDbSet<Device> Devices { get; set; }
        public IDbSet<DeviceAreaFilter> DeviceAreaFilter { get; set; }
        public IDbSet<DevicePokemonFilter> DevicePokemonFilter { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

    public enum RegisterOrUpdateResponseEnum
    {
        Registered,
        Updated,
        Error
    }






}