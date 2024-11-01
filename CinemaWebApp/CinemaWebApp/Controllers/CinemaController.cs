namespace CinemaWebApp.Web.Controllers
{
    using CinemaWebApp.Controllers;
    using CinemaWebApp.Services.Data.Contracts;
    using CinemaWebApp.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Mvc;

    public class CinemaController : BaseController
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> cinemas =
                await this.cinemaService.IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaService.IndexGetAllOrderedByLocationAsync();
            return this.View(cinemas);
        }

        [HttpGet]
#pragma warning disable CS1998
        public async Task<IActionResult> Create()
#pragma warning restore CS1998
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.cinemaService.AddCinemaAsync(model);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel? viewModel = await this.cinemaService
                .GetCinemaDetailsByIdAsync(cinemaGuid);

            // Invalid(non-existing) GUID in the URL
            if (viewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(viewModel);
        }

        // GET: Cinema/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var cinemaGuid))
            {
                return RedirectToAction(nameof(Manage));
            }

            EditCinemaFormModel? cinema = await this.cinemaService.GetCinemaEditModelByIdAsync(cinemaGuid);

            if (cinema == null)
            {
                return RedirectToAction(nameof(Manage));
            }

            return View(cinema);
        }

        // POST: Cinema/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditCinemaFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id != model.Id)
            {
                // Handle case where ID does not match
                return RedirectToAction(nameof(Manage));
            }

            bool isUpdated = await this.cinemaService.UpdateCinemaAsync(model);

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update cinema.");
                return View(model);
            }

            return RedirectToAction(nameof(Manage));
        }

        // GET: Cinema/SoftDelete/{id}
        [HttpGet]
        public async Task<IActionResult> SoftDelete(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var cinemaGuid))
            {
                return RedirectToAction(nameof(Manage));
            }

            CinemaDetailsViewModel? cinema = await this.cinemaService.GetCinemaDetailsByIdAsync(cinemaGuid);

            if (cinema == null)
            {
                return RedirectToAction(nameof(Manage));
            }

            return View(cinema); // Display the confirmation view
        }

        // POST: Cinema/SoftDelete/{id}
        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out var cinemaGuid))
            {
                return RedirectToAction(nameof(Manage));
            }

            bool isSoftDeleted = await this.cinemaService.SoftDeleteCinemaAsync(cinemaGuid);

            if (!isSoftDeleted)
            {
                // Display an error message if deletion was restricted
                TempData["ErrorMessage"] = "Unable to delete cinema. It may have active movies associated with it.";
                return RedirectToAction(nameof(SoftDelete), new { id = id });
            }

            return RedirectToAction(nameof(Manage));
        }

    }
}