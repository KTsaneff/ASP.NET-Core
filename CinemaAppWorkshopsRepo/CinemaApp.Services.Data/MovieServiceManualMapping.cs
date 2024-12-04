namespace CinemaApp.Services.Data
{
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Web.ViewModels.Movie;
    using CinemaApp.Web.ViewModels.Cinema;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using CinemaApp.Services.Data.Interfaces;

    public class MovieServiceManualMapping : IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;
        private readonly IRepository<Cinema, Guid> cinemaRepository;
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public MovieServiceManualMapping(
            IRepository<Movie, Guid> movieRepository,
            IRepository<Cinema, Guid> cinemaRepository,
            IRepository<CinemaMovie, object> cinemaMovieRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.cinemaMovieRepository = cinemaMovieRepository;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            var movies = await this.movieRepository.GetAllAttached().ToListAsync();
            return movies.Select(movie => new AllMoviesIndexViewModel
            {
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
                Duration = $"{movie.Duration}"
            });
        }

        public async Task<(IEnumerable<AllMoviesIndexViewModel> Movies, int TotalPages)> GetAllMoviesAsync(
            string? searchQuery = null,
            string? genre = null,
            int? releaseYear = null,
            int pageNumber = 1,
            int pageSize = 5)
        {
            IQueryable<Movie> movies = this.movieRepository.GetAllAttached();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                movies = movies.Where(m => m.Title.ToLower().Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                genre = genre.ToLower().Trim();
                movies = movies.Where(m => m.Genre.ToLower() == genre);
            }

            if (releaseYear.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Year == releaseYear.Value);
            }

            int totalMovies = await movies.CountAsync();
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            movies = movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var moviesToReturn = await movies.ToListAsync();

            var moviesViewModel = moviesToReturn.Select(movie => new AllMoviesIndexViewModel
            {
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
                Duration = $"{movie.Duration}"
            });

            return (moviesViewModel, totalPages);
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            var movie = await this.movieRepository.GetByIdAsync(id);
            if (movie == null)
                return null;

            return new MovieDetailsViewModel
            {
                Title = movie.Title,
                Genre = movie.Genre,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate.ToString("yyyy-MM-dd"),
                Duration = $"{movie.Duration}"
            };
        }

        public async Task<bool> AddMovieAsync(AddMovieInputModel inputModel)
        {
            if (!DateTime.TryParseExact(
                    inputModel.ReleaseDate,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime releaseDate))
            {
                return false;
            }

            var movie = new Movie
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                Description = inputModel.Description,
                ReleaseDate = releaseDate,
                Duration = inputModel.Duration
            };

            await this.movieRepository.AddAsync(movie);
            return true;
        }

        public async Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id)
        {
            var movie = await this.movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            var cinemas = await this.cinemaRepository.GetAllAttached()
                .Include(c => c.CinemaMovies)
                .Select(c => new CinemaCheckBoxItemInputModel
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                    IsSelected = c.CinemaMovies.Any(cm => cm.MovieId == id && !cm.IsDeleted)
                })
                .ToListAsync();

            return new AddMovieToCinemaInputModel
            {
                Id = movie.Id.ToString(),
                MovieTitle = movie.Title,
                Cinemas = cinemas
            };
        }

        public async Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model)
        {
            var movie = await this.movieRepository.GetByIdAsync(movieId);
            if (movie == null)
            {
                return false;
            }

            foreach (var cinemaInputModel in model.Cinemas)
            {
                if (!Guid.TryParse(cinemaInputModel.Id, out Guid cinemaId))
                {
                    continue;
                }

                var cinema = await this.cinemaRepository.GetByIdAsync(cinemaId);
                if (cinema == null)
                {
                    continue;
                }

                var cinemaMovie = await this.cinemaMovieRepository.FirstOrDefaultAsync(
                    cm => cm.MovieId == movieId && cm.CinemaId == cinemaId);

                if (cinemaInputModel.IsSelected)
                {
                    if (cinemaMovie == null)
                    {
                        await this.cinemaMovieRepository.AddAsync(new CinemaMovie
                        {
                            CinemaId = cinemaId,
                            MovieId = movieId,
                            AvailableTickets = 0
                        });
                    }
                    else if (cinemaMovie.IsDeleted)
                    {
                        cinemaMovie.IsDeleted = false;
                    }
                }
                else
                {
                    if (cinemaMovie != null && !cinemaMovie.IsDeleted)
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }
            }

            await this.cinemaMovieRepository.SaveChangesAsync();
            return true;
        }

    }
}
