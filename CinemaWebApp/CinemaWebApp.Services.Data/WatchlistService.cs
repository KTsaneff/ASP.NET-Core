using CinemaWebApp.Data.Models;
using CinemaWebApp.Data.Repository.Contracts;
using CinemaWebApp.Services.Data.Contracts;
using CinemaWebApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;
using static CinemaWebApp.Common.EntityValidationConstants.Movie;

namespace CinemaWebApp.Services.Data
{
    public class WatchlistService : BaseService, IWatchlistService
    {
        private readonly IRepository<ApplicationUserMovie, Guid> userMovieRepository;

        public WatchlistService(IRepository<ApplicationUserMovie, Guid> userMovieRepository)
        {
            this.userMovieRepository = userMovieRepository;
        }

        public async Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchlistByUserIdAsync(string userId)
        {
            IEnumerable<ApplicationUserWatchlistViewModel> watchlist = await this.userMovieRepository
               .GetAllAttached()
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

            return watchlist;
        }
        public Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveMovieFromUserWatchlistAsync(string? movieId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
