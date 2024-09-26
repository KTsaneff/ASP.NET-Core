using CinemaWebApp.Models.Data;
using CinemaWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApp.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext _context;

        public CinemaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Fetch the list of cinemas from the database
            var cinemas = _context.Cinemas.ToList();

            //Map the Cinema entities to CinemaIndexViewModel
            var cinemaIndexViewModels = cinemas.Select(c => new CinemaIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            });

            //Pass the list of CinemaIndexViewModel to the view
            return View(cinemaIndexViewModels);
        }
    }
}
