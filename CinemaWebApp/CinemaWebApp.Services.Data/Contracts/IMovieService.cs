using CinemaWebApp.Web.ViewModels.Movie;

namespace CinemaWebApp.Services.Data.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task<bool> AddMovieAsync(AddMovieInputModel inputModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);

        Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id);

        Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model);

        Task<EditMovieFormModel?> GetMovieEditModelByIdAsync(Guid id);

        Task<bool> UpdateMovieAsync(EditMovieFormModel model);

        Task<bool> SoftDeleteMovieAsync(Guid id);
    }
}
