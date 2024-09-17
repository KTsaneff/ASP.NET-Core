using Microsoft.AspNetCore.Mvc;
using CinemaWebApp.Models; //Include the Movie model
using System.Collections.Generic; //For handling lists

namespace CinemaWebApp.Controllers
{
    public class MovieController : Controller
    {
        private static List<Movie> movies = new List<Movie>();

        public IActionResult Index()
        {
            return View(movies); //Pass the list of movies to the view
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Action to handle form submission for creating a new movie
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            //Assign a unique id to the movie
            movie.Id = movies.Count + 1;

            //Add the movie to the list
            movies.Add(movie);

            //Redirect to the Index action
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Movie movie = movies.Find(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
