﻿namespace CinemaWebApp.Web.Controllers
{
    using CinemaWebApp.Controllers;
    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Models.Data;
    using CinemaWebApp.Web.ViewModels.Watchlist;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using static Common.EntityValidationConstants.Movie;

    [Authorize]
    public class WatchlistController : BaseController
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User)!;

            IEnumerable<ApplicationUserWatchlistViewModel> watchlist = await this.dbContext
                .UsersMovies
                .Include(um => um.Movie)
                .Where(um => um.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .Select(um => new ApplicationUserWatchlistViewModel()
                {
                    MovieId = um.MovieId.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToListAsync();

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