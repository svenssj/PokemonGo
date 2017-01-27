using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ComminityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;


namespace ComminityNotifier.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISightnigsApplicationService _applicationService;

        public HomeController(ISightnigsApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {

            var sightingsViewModel = new SightingsListViewModel();
            var sightings = await _applicationService.GetReports();
            sightingsViewModel.Sightings = new List<SightingsReportDTO>();

            foreach (var sightingsReport in sightings)
            {
                sightingsViewModel.Sightings.Add(new SightingsReportDTO()
                {
                    Area = new AreaDTO() { AreaName = sightingsReport.Area.AreaName,Id=sightingsReport.Area.AreaId},
                    Locaiton = sightingsReport.Locaiton,
                    PokemonName = sightingsReport.Pokemon.PokemonName,
                    ReportTime = sightingsReport.ReportTime
                });
            }

            sightingsViewModel.Pokemons = new List<PokemonDTO>();
            var pokemons = await _applicationService.GetPokemons();
            foreach (var p in pokemons)
            {
                sightingsViewModel.Pokemons.Add(new PokemonDTO() {Name = p.PokemonName,Number = p.PokemonNumber});
            }

            sightingsViewModel.Areas = new List<AreaDTO>();
           var areas= await _applicationService.GetAreas();
            foreach (var area in areas)
            {
                sightingsViewModel.Areas.Add(new AreaDTO() {AreaName = area.AreaName,Id=area.AreaId});
            }

            return View(sightingsViewModel);
        }


        public async Task<ActionResult> AddReport(int pokemon, int areaId, string location)
        {
            try
            {
               await _applicationService.ReportSighting(pokemon, areaId, location, DateTime.UtcNow);
            }
            catch (Exception)
            {
                
           return new HttpStatusCodeResult(500);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}