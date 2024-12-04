using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CinemaApp.Tests
{
    [TestFixture]
    public class MovieServiceManualMappingTest
    {
        private Mock<IRepository<Movie, Guid>> mockMovieRepository;
        private Mock<IRepository<Cinema, Guid>> mockCinemaRepository;
        private Mock<IRepository<CinemaMovie, object>> mockCinemaMovieRepository;

        private MovieServiceManualMapping movieService;

        [SetUp]
        public void Setup()
        {
            this.mockMovieRepository = new Mock<IRepository<Movie, Guid>>();
            this.mockCinemaRepository = new Mock<IRepository<Cinema, Guid>>();
            this.mockCinemaMovieRepository = new Mock<IRepository<CinemaMovie, object>>();

            this.movieService = new MovieServiceManualMapping(
                this.mockMovieRepository.Object,
                this.mockCinemaRepository.Object,
                this.mockCinemaMovieRepository.Object);
        }

        [Test]
        public async Task GetAllMoviesAsync_ShouldReturnMappedMovies_WhenMoviesExist()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Duration = 148
                }
            }.AsQueryable();

            var mockMovies = new Mock<DbSet<Movie>>();
            mockMovies.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movies.Provider);
            mockMovies.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movies.Expression);
            mockMovies.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movies.ElementType);
            mockMovies.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movies.GetEnumerator());

            this.mockMovieRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockMovies.Object);

            var result = await this.movieService.GetAllMoviesAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
            var movie = result.First();
            Assert.That(movie.Title, Is.EqualTo("Inception"));
            Assert.That(movie.Genre, Is.EqualTo("Sci-Fi"));
            Assert.That(movie.ReleaseDate, Is.EqualTo("2010-07-16"));
            Assert.That(movie.Duration, Is.EqualTo("148"));
        }
    }
}
