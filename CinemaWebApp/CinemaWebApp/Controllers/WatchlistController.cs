using CinemaWebApp.Data.Models;
using CinemaWebApp.Models;
using CinemaWebApp.Models.Data;
using CinemaWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WatchlistController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var watchListMovies = await _context.UsersMovies
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie)
                .Select(um => new WatchlistViewModel
                {
                    MovieId = um.MovieId,
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString("MMMM yyyy"),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToListAsync();

            return View(watchListMovies);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);

            var userMovie = await _context.UsersMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie == null)
            {
                userMovie = new UserMovie
                {
                    UserId = userId,
                    MovieId = movieId
                };

                _context.UsersMovies.Add(userMovie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Movie");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);

            var userMovie = await _context.UsersMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (userMovie != null)
            {
                _context.UsersMovies.Remove(userMovie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
