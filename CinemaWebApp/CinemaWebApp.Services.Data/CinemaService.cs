﻿namespace CinemaWebApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Data.Repository.Interfaces;
    using Interfaces;
    using Mapping;
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

        public async Task<EditCinemaFormModel?> GetCinemaForEditByIdAsync(Guid id)
        {
            EditCinemaFormModel? cinemaModel = await this.cinemaRepository
                .GetAllAttached()
                .To<EditCinemaFormModel>()
                .FirstOrDefaultAsync(c => c.Id.ToLower() == id.ToString().ToLower());

            return cinemaModel;
        }

        public async Task<bool> EditCinemaAsync(EditCinemaFormModel model)
        {
            Cinema cinemaEntity = AutoMapperConfig.MapperInstance
                .Map<EditCinemaFormModel, Cinema>(model);

            bool result = await this.cinemaRepository.UpdateAsync(cinemaEntity);
            return result;
        }

        public async Task<CinemaProgramViewModel?> GetCinemaProgramByIdAsync(Guid id)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .Include(c => c.CinemaMovies)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);

            CinemaProgramViewModel? viewModel = null;
            if (cinema != null)
            {
                viewModel = new CinemaProgramViewModel()
                {
                    Id = cinema.Id.ToString(),
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema.CinemaMovies
                        .Where(cm => !cm.IsDeleted)
                        .Select(cm => new MovieInCinemaViewModel
                        {
                            Id = cm.Movie.Id.ToString(),
                            CinemaId = cm.CinemaId.ToString(),
                            Title = cm.Movie.Title,
                            Genre = cm.Movie.Genre,
                            Duration = $"{cm.Movie.Duration} min",
                            Description = cm.Movie.Description,
                            AvailableTickets = cm.AvailableTickets
                        })
                        .ToList()
                };
            }

            return viewModel;
        }

    }
}