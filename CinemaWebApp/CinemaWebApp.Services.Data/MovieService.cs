namespace CinemaWebApp.Services.Data
{
    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Data.Repository.Interfaces;
    using CinemaWebApp.Services.Data.Interfaces;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using Web.ViewModels.Cinema;
    using Web.ViewModels.Movie;
    using static Common.EntityValidationConstants.Movie;

    public class MovieService : BaseService, IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;
        private readonly IRepository<Cinema, Guid> cinemaRepository;
        private readonly IRepository<CinemaMovie, Guid> cinemaMovieRepository;

        public MovieService(IRepository<Movie, Guid> movieRepository,
            IRepository<Cinema, Guid> cinemaRepository,
            IRepository<CinemaMovie, Guid> cinemaMovieRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.cinemaMovieRepository = cinemaMovieRepository;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await movieRepository
                .GetAllAttached()
                .Where(m => m.IsDeleted == false)
                .To<AllMoviesIndexViewModel>()
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(AddMovieInputModel inputModel)
        {
            bool isReleaseDateValid = DateTime
                .TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime releaseDate);
            if (!isReleaseDateValid)
            {
                return false;
            }

            Movie movie = new Movie();
            AutoMapperConfig.MapperInstance.Map(inputModel, movie);
            movie.ReleaseDate = releaseDate;

            await this.movieRepository.AddAsync(movie);

            return true;
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            Movie? movie = await this.movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null;
            }

            MovieDetailsViewModel viewModel = new MovieDetailsViewModel();
            AutoMapperConfig.MapperInstance.Map(movie, viewModel);

            return viewModel;
        }


        public async Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);
            AddMovieToCinemaInputModel? viewModel = null;
            if (movie != null)
            {
                viewModel = new AddMovieToCinemaInputModel()
                {
                    Id = id.ToString(),
                    MovieTitle = movie.Title,
                    Cinemas = await this.cinemaRepository
                        .GetAllAttached()
                        .Include(c => c.CinemaMovies)
                        .ThenInclude(cm => cm.Movie)
                        .Select(c => new CinemaCheckBoxItemInputModel()
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            Location = c.Location,
                            IsSelected = c.CinemaMovies
                                .Any(cm => cm.Movie.Id == id &&
                                           cm.IsDeleted == false)
                        })
                        .ToArrayAsync()
                };
            }

            return viewModel;
        }

        public async Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieId);
            if (movie == null)
            {
                return false;
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();
            foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;
                bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);
                if (!isCinemaGuidValid)
                {
                    return false;
                }

                Cinema? cinema = await this.cinemaRepository
                    .GetByIdAsync(cinemaGuid);
                if (cinema == null)
                {
                    return false;
                }

                CinemaMovie? cinemaMovie = await this.cinemaMovieRepository
                    .FirstOrDefaultAsync(cm => cm.MovieId == movieId &&
                                                     cm.CinemaId == cinemaGuid);
                if (cinemaInputModel.IsSelected)
                {
                    if (cinemaMovie == null)
                    {
                        entitiesToAdd.Add(new CinemaMovie()
                        {
                            Cinema = cinema,
                            Movie = movie
                        });
                    }
                    else
                    {
                        cinemaMovie.IsDeleted = false;
                    }
                }
                else
                {
                    if (cinemaMovie != null)
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }
            }

            await this.cinemaMovieRepository.AddRangeAsync(entitiesToAdd.ToArray());

            return true;
        }

        public async Task<EditMovieFormModel?> GetMovieEditModelByIdAsync(Guid id)
        {
            var movie = await movieRepository.GetByIdAsync(id);

            if (movie == null)
            {
                return null; // Movie not found
            }

            // Use AutoMapper to map the movie to EditMovieFormModel
            return AutoMapperConfig.MapperInstance.Map<EditMovieFormModel>(movie);
        }

        public async Task<bool> UpdateMovieAsync(EditMovieFormModel model)
        {
            var movie = await movieRepository.GetByIdAsync(model.Id);
            if (movie == null)
            {
                return false; // Movie not found
            }

            // Manual mapping
            movie.Title = model.Title;
            movie.Genre = model.Genre;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Duration = model.Duration;
            movie.Director = model.Director;
            movie.Description = model.Description;
            movie.ImageUrl = model.ImageUrl;

            return await movieRepository.UpdateAsync(movie); // Assuming UpdateAsync returns bool
        }

        public async Task<bool> SoftDeleteMovieAsync(Guid id)
        {
            var movie = await this.movieRepository.GetAllAttached()
                .Include(m => m.MovieCinemas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return false; // Movie not found
            }

            bool isShowingInCinemas = movie.MovieCinemas.Any(mc => !mc.IsDeleted);
            if (isShowingInCinemas)
            {
                return false; // Restriction: movie is currently showing in cinemas
            }

            movie.IsDeleted = true;

            await this.movieRepository.UpdateAsync(movie);

            return true;
        }

    }
}