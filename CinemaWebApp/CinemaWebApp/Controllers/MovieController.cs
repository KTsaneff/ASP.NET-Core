using Microsoft.AspNetCore.Mvc;
using CinemaWebApp.Models; //Include the Movie model
using System.Collections.Generic;
using CinemaWebApp.Models.Data;
using CinemaWebApp.ViewModels; //For handling lists

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
            return View(new MovieViewModel());
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel viewModel)
        {
            //Validate the input data using ModelState
            if (ModelState.IsValid)
            {
                //Map the view model to the Movie entity
                var movie = new Movie
                {
                    Title = viewModel.Title,
                    Genre = viewModel.Genre,
                    ReleaseDate = viewModel.ReleaseDate,
                    Director = viewModel.Director,
                    Duration = viewModel.Duration,
                    Description = viewModel.Description
                };

                _context.Movies.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
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
