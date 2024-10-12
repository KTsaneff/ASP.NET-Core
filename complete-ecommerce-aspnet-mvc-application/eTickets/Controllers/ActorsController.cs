using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;

        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        public async Task<IActionResult> Index()
        {
            var allActors = await _actorsService.GetAll();
            return View(allActors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
