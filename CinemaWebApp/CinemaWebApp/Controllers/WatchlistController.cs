namespace CinemaWebApp.Web.Controllers
{
    using CinemaWebApp.Controllers;
    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Models.Data;
    using CinemaWebApp.Services.Data.Contracts;
    using CinemaWebApp.Web.ViewModels.Watchlist;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(IWatchlistService watchlistService, 
                                   AppDbContext dbContext, 
                                   UserManager<ApplicationUser> userManager)
        {
            this.watchlistService = watchlistService;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User)!;

            if(string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

           IEnumerable<ApplicationUserWatchlistViewModel> watchlist = await this.watchlistService.GetUserWatchlistByUserIdAsync(userId);

            return View(watchlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(string? movieId)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return this.RedirectToAction("Index", "Movie");
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);
            if (movie == null)
            {
                return this.RedirectToAction("Index", "Movie");
            }

            Guid userGuid = Guid.Parse(this.userManager.GetUserId(this.User)!);

            bool addedToWatchlistAlready = await this.dbContext
                .UsersMovies
                .AnyAsync(um => um.ApplicationUserId == userGuid &&
                                um.MovieId == movieGuid);
            if (!addedToWatchlistAlready)
            {
                ApplicationUserMovie newUserMovie = new ApplicationUserMovie()
                {
                    ApplicationUserId = userGuid,
                    MovieId = movieGuid
                };

                await this.dbContext.UsersMovies.AddAsync(newUserMovie);
                await this.dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(string? movieId)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return this.RedirectToAction("Index", "Movie");
            }

            Movie? movie = await this.dbContext
                .Movies
                .FirstOrDefaultAsync(m => m.Id == movieGuid);
            if (movie == null)
            {
                return this.RedirectToAction("Index", "Movie");
            }

            Guid userGuid = Guid.Parse(this.userManager.GetUserId(this.User)!);

            ApplicationUserMovie? applicationUserMovie = await this.dbContext
                .UsersMovies
                .FirstOrDefaultAsync(um => um.ApplicationUserId == userGuid &&
                                um.MovieId == movieGuid);
            if (applicationUserMovie != null)
            {
                this.dbContext.UsersMovies.Remove(applicationUserMovie);
                await this.dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(Index));
        }
    }
}