using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ComminityNotifier.Api.Models;
using CommunityNotifier.Core.ApplicationService;


namespace ComminityNotifier.Api.Controllers
{
    public class NestsController : Controller
    {
        private readonly ISightnigsApplicationService _applicationService;

        public NestsController(ISightnigsApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: Nests
        public async Task<ActionResult> Index()
        {
            var nests = await _applicationService.GetNestReports();
            var model = new NestListViewModel()
            {
                Areas = (await _applicationService.GetAreas()).Select(a =>
                    new AreaDTO()
                    {
                        AreaName = a.AreaName,
                        Id = a.AreaId
                    }).ToList(),

                Pokemons = (await _applicationService.GetPokemons()).Select(p =>
                    new PokemonDTO()
                    {
                        Name = p.PokemonName,
                        Number = p.PokemonNumber
                    }).ToList(),
                NestReports = nests.Select(n =>
                    new NestReportDTO()
                    {
                        Location = n.Locations.Select(l =>
                            new LocationDTO()
                            {
                                Area = l.Area.AreaName,
                                Spot = l.Spot
                            }).ToList(),
                        Pokemon = new PokemonDTO()
                        {
                            Name = n.Pokemon.PokemonName,
                            Number = n.Pokemon.PokemonNumber
                        }
                    }).ToList()

            };

            return View(model);
        }


        public async Task<ActionResult> AddNestReport(int pokemonid, int areaid, string spot)
        {
            try
            {
  await _applicationService.AddNestReport(pokemonid, areaid, spot);
                return RedirectToAction("Index", "Nests");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500);
            }
          
        }
    }
}
