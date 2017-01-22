using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.Repository
{
    
    internal class Repository : IRepository
    {
        private readonly SightingsContext _sightingsContext;

        public Repository()
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
          query=  query.Include("Area");
            query = query.Include("Pokemon");

            var list = await query.ToListAsync();
            var newList = new List<SightingsReport>();

            foreach (var sightingsReport in list)
            {

                var span = (now -sightingsReport.ReportTime).TotalMinutes;
                if(span<60)
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
            return  await _sightingsContext.SaveChangesAsync();
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
 

   


}