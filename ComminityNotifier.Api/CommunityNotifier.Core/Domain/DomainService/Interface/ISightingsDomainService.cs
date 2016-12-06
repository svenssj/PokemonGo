using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.Domain.DomainService.Interface
{
    public interface ISightingsDomainService
    {
        Task<int> AddSightingsReport(SightingsReport sighting);
        Task<List<SightingsReport>> GetSightingReports();
    }
}