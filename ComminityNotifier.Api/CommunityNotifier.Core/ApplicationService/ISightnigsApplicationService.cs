using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityNotifier.Core.Domain.Model;

namespace CommunityNotifier.Core.ApplicationService
{
    public interface ISightnigsApplicationService
    {
        Task<int> ReportSighting(int pokemonId, int areaId, string location,DateTime reportTime);
        Task<List<SightingsReport>> GetReports();
        Task<List<Area>> GetAreas();
        Task<List<Pokemon>>  GetPokemons();
        Task<bool> AddRegistrationId(string deviceId,string registrationId);
    }
}