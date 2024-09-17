using Microsoft.AspNetCore.Mvc;
using CinemaWebApp.Models; //Include the Movie model
using System.Collections.Generic;
using CinemaWebApp.Models.Data; //For handling lists

namespace CinemaWebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;

        //Inject the AppDbContext using constructor dependency injection
        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList(); // Retrieve all movies from the database
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if(ModelState.IsValid)
            {
                _context.Movies.Add(movie); // Add the movie to the database
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the Index action
            }
            return View(movie); // Return the view with the movie model
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Find(id); // Find the movie by id

            if(movie == null)
            {
                return NotFound(); // Return 404 Not Found
            }

            return View(movie);
        }   
    }
}
