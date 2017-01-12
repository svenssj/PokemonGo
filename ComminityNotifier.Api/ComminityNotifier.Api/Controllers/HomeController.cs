using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ComminityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;
using CommunityNotifier.Core.Domain.Model;

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
                    Area = sightingsReport.Area.ToString(),
                    Locaiton = sightingsReport.Locaiton,
                    PokemonName = sightingsReport.Pokemon.ToString(),
                    ReportTime = sightingsReport.ReportTime
                });
            }

            sightingsViewModel.Pokemons = new List<PokemonEnum>();

            foreach (PokemonEnum e in Enum.GetValues(typeof(PokemonEnum)))
            {
                sightingsViewModel.Pokemons.Add(e);
            }
            sightingsViewModel.Areas = new List<AreaEnum>();
            foreach (AreaEnum e in Enum.GetValues(typeof(AreaEnum)))
            {
                sightingsViewModel.Areas.Add(e);
            }

            return View(sightingsViewModel);
        }


        public async Task<ActionResult> AddReport(int pokemon, int area, string location)
        {
            try
            {
               await _applicationService.ReportSighting(pokemon, ((AreaEnum)area).ToString(), location, DateTime.Now);
            }
            catch (Exception)
            {
                
           return new HttpStatusCodeResult(500);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}