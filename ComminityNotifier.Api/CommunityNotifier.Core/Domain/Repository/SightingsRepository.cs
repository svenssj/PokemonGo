using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.Repository
{
    
    internal class SightingsRepository : ISightingsRepository
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

        public async Task<List<SightingsReport>> GetReportsAsList()
        {
            var now = DateTime.UtcNow;
            return await _sightingsContext.SightingsReports.Where(rep=>(now.Subtract(rep.ReportTime).TotalMinutes<60)).ToListAsync();
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
    }

    internal interface ISightingsRepository : IDisposable
    {
        Task<List<SightingsReport>> GetReportsAsList();
        Guid AddReport(SightingsReport reportToAdd);
        void RemoveReport(Guid guid);
        void RemoveReport(SightingsReport reportToRemove);
        void SaveChanges();
        Task<int> SaveChangesAsync();

    }

    public class SightingsContext : DbContext
    {

        public SightingsContext()
        {
                Database.SetInitializer<SightingsContext>(null);
        }
       public IDbSet<SightingsReport> SightingsReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

  
}