namespace CinemaWebApp.Services.Data.Interfaces
{
    using CinemaWebApp.Web.ViewModels.Watchlist;

    public interface IWatchlistService
    {
        Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchListByUserIdAsync(string userId);

        Task<bool> AddMovieToUserWatchListAsync(string? movieId, string userId);

        Task<bool> RemoveMovieFromUserWatchListAsync(string? movieId, string userId);
    }
}