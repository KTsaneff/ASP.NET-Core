using CinemaWebApp.Models;

namespace CinemaWebApp.Infrastructure.Repositories.Contracts
{
    public interface IMovieRepository : IRepository<Movie>
    {
        // Add additional movie-specific data access methods here if needed
    }
}
