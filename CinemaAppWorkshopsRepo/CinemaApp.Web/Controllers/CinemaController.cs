namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Cinema;

    public class CinemaController : BaseController
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService, IManagerService managerService)
            : base(managerService)
        {
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool isManager = await this.IsUserManagerAsync();
            if (isManager)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            IEnumerable<CinemaIndexViewModel> cinemas =
                await this.cinemaService.IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (isManager)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel? viewModel = await this.cinemaService
                .GetCinemaDetailsByIdAsync(cinemaGuid);

            if (viewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewProgram(Guid id)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (isManager)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            CinemaProgramViewModel? viewModel = await this.cinemaService.GetCinemaProgramByIdAsync(id);

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<CinemaIndexViewModel> cinemas =
                await this.cinemaService.IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(AddCinemaFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.cinemaService.AddCinemaAsync(model);

            return this.RedirectToAction(nameof(Manage));
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(string? id)
        {
            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            EditCinemaFormModel? formModel = await this.cinemaService
                .GetCinemaForEditByIdAsync(cinemaGuid);

            return this.View(formModel);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(EditCinemaFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(formModel);
            }

            bool isUpdated = await this.cinemaService
                .EditCinemaAsync(formModel);
            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the cinema! Please contact administrator");
                return this.View(formModel);
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
