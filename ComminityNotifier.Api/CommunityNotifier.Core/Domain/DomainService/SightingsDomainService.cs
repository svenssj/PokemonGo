using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.DomainService.Interface;
using CommunityNotifier.Core.Domain.Model;
using CommunityNotifier.Core.Domain.Repository;

namespace CommunityNotifier.Core.Domain.DomainService
{
    internal class SightingsDomainService : ISightingsDomainService
    {
        private readonly ISightingsRepository _repository;
        public SightingsDomainService(ISightingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddSightingsReport(SightingsReport sighting)
        {
            _repository.AddReport(sighting);
            return await _repository.SaveChangesAsync();
          
        }

        public async Task<List<SightingsReport>> GetSightingReports()
        {
            return await _repository.GetReportsAsList();
        }
    }
}
