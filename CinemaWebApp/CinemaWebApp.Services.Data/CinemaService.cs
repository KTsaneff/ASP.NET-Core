namespace CinemaWebApp.Services.Data
{
    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Data.Repository.Contracts;
    using CinemaWebApp.Services.Data.Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Cinema;
    using Web.ViewModels.Movie;

    public class CinemaService : BaseService, ICinemaService
    {
        private readonly IRepository<Cinema, Guid> cinemaRepository;

        public CinemaService(IRepository<Cinema, Guid> cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaRepository
                .GetAllAttached()
                .OrderBy(c => c.Location)
                .To<CinemaIndexViewModel>()
                .ToArrayAsync();

            return cinemas;
        }

        public async Task AddCinemaAsync(AddCinemaFormModel model)
        {
            Cinema cinema = new Cinema();
            AutoMapperConfig.MapperInstance.Map(model, cinema);

            await this.cinemaRepository.AddAsync(cinema);
        }

        public async Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);

            CinemaDetailsViewModel? viewModel = null;
            if (cinema != null)
            {
                viewModel = new CinemaDetailsViewModel()
                {
                    Id = cinema.Id, // Populate the Id here
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema.CinemaMovies
                        .Where(cm => cm.IsDeleted == false)
                        .Select(cm => new CinemaMovieViewModel()
                        {
                            Title = cm.Movie.Title,
                            Duration = cm.Movie.Duration,
                        })
                        .ToArray()
                };
            }

            return viewModel;
        }


        public async Task<EditCinemaFormModel?> GetCinemaEditModelByIdAsync(Guid id)
        {
            var cinema = await this.cinemaRepository
                .GetAllAttached()
                .Where(c => c.Id == id)
                .Select(c => new EditCinemaFormModel
                {
                    Name = c.Name,
                    Location = c.Location
                })
                .FirstOrDefaultAsync();

            return cinema;
        }

        public async Task<bool> UpdateCinemaAsync(EditCinemaFormModel model)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(c => c.Id.ToString() == model.Id);

            if (cinema == null)
            {
                return false;
            }

            cinema.Name = model.Name;
            cinema.Location = model.Location;

            await this.cinemaRepository.UpdateAsync(cinema);

            return true;
        }

        public async Task<bool> SoftDeleteCinemaAsync(Guid id)
        {
            var cinema = await this.cinemaRepository.GetAllAttached()
                .Include(c => c.CinemaMovies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cinema == null)
            {
                return false;
            }

            bool hasActiveMovies = cinema.CinemaMovies.Any(cm => !cm.IsDeleted);
            if (hasActiveMovies)
            {
                return false; // Restriction: cinema has active movies
            }

            cinema.IsDeleted = true;

            await this.cinemaRepository.UpdateAsync(cinema);

            return true;
        }
    }
}