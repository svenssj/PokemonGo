using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.ApplicationService
{
    public interface ISightnigsApplicationService
    {
        Task<int> ReportSighting(int pokemonId, string area, string location,DateTime reportTime);
        Task<List<SightingsReport>> GetReports();
        Task<List<string>> GetAreas();
    }
}