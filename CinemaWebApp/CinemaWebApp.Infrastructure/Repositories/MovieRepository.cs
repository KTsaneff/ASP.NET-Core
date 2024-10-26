using CinemaWebApp.Infrastructure.Repositories.Contracts;
using CinemaWebApp.Models;
using CinemaWebApp.Models.Data;

namespace CinemaWebApp.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context)
        {
        }
    }
}
