namespace CinemaWebApp.Web.Controllers
{
    using CinemaWebApp.Controllers;
    using CinemaWebApp.Models.Data;
    using CinemaWebApp.Services.Data;
    using CinemaWebApp.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Movie;
    using static Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private readonly AppDbContext dbContext;
        private readonly IMovieService movieService;

        public MovieController(AppDbContext dbContext, IMovieService movieService)
        {
            this.dbContext = dbContext;
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies =
                await this.movieService.GetAllMoviesAsync();

            return this.View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<AllMoviesIndexViewModel> movies = await this.movieService.GetAllMoviesAsync();
            return this.View(movies);
        }

        [HttpGet]
#pragma warning disable CS1998
        public async Task<IActionResult> Create()
#pragma warning restore CS1998
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Render the same form with user entered values + model errors 
                return this.View(inputModel);
            }

            bool result = await this.movieService.AddMovieAsync(inputModel);
            if (result == false)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate),
                    String.Format("The Release Date must be in the following format: {0}", ReleaseDateFormat));
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                // Invalid id format
                return this.RedirectToAction(nameof(Index));
            }

            MovieDetailsViewModel? movie = await this.movieService
                .GetMovieDetailsByIdAsync(movieGuid);
            if (movie == null)
            {
                // Non-existing movie guid
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        // GET: Movie/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var movieGuid))
            {
                return RedirectToAction(nameof(Index));
            }

            EditMovieFormModel? model = await movieService.GetMovieEditModelByIdAsync(movieGuid);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: Movie/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMovieFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUpdated = await movieService.UpdateMovieAsync(model);

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update the movie.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel? viewModel = await this.movieService
                .GetAddMovieToCinemaInputModelByIdAsync(movieGuid);
            if (viewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(model.Id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool result = await this.movieService
                .AddMovieToCinemasAsync(movieGuid, model);
            if (result == false)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index), "Cinema");
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(string? id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var movieGuid))
            {
                return RedirectToAction(nameof(Manage));
            }

            MovieDetailsViewModel? movie = await movieService.GetMovieDetailsByIdAsync(movieGuid);

            if (movie == null)
            {
                return RedirectToAction(nameof(Manage));
            }

            return View(movie);
        }

        // POST: Movie/SoftDelete/{id}
        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out var movieGuid))
            {
                return RedirectToAction(nameof(Index));
            }

            bool isSoftDeleted = await this.movieService.SoftDeleteMovieAsync(movieGuid);

            if (!isSoftDeleted)
            {
                // Display an error message if deletion was restricted
                TempData["ErrorMessage"] = "Unable to delete movie. It may be currently showing in cinemas.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            return RedirectToAction(nameof(Index)); // Redirect to the movie list or any other appropriate view
        }
    }
}