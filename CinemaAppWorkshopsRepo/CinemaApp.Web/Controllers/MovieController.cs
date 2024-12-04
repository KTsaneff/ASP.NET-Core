namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Services.Data.Interfaces;
    using ViewModels.Movie;

    using static Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService, IManagerService managerService)
            : base(managerService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string? searchQuery = null, string? genre = null, int? releaseYear = null, int pageNumber = 1)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (isManager)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            (IEnumerable<AllMoviesIndexViewModel> movies, int totalPages) =
                await this.movieService.GetAllMoviesAsync(searchQuery, genre, releaseYear, pageNumber);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["Genre"] = genre;
            ViewData["ReleaseYear"] = releaseYear;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return this.View(movies);
        }




        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string? id)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (isManager)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            MovieDetailsViewModel? movie = await this.movieService
                .GetMovieDetailsByIdAsync(movieGuid);
            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies =
                await this.movieService.GetAllMoviesAsync();

            return this.View(allMovies);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            bool result = await this.movieService.AddMovieAsync(inputModel);
            if (!result)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate),
                    String.Format("The Release Date must be in the following format: {0}", ReleaseDateFormat));
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Manage));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
            if (!result)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
