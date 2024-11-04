using CinemaWebApp.Web.ViewModels.Watchlist;

namespace CinemaWebApp.Services.Data.Contracts
{
    public interface IWatchlistService
    {
        Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchlistByUserIdAsync(string userId);

        Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string userId);

        Task<bool> RemoveMovieFromUserWatchlistAsync(string? movieId, string userId);
    }
}
