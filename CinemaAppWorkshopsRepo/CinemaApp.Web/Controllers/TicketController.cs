namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Tickets;

    public class TicketController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ICinemaService cinemaService;

        public TicketController(ITicketService ticketService, ICinemaService cinemaService, IManagerService managerService)
            : base(managerService)
        {
            this.ticketService = ticketService;
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyTickets()
        {
            string userId = this.User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            Guid userGuid = Guid.Parse(userId);
            var tickets = await this.ticketService.GetUserTicketsAsync(userGuid);

            return View(tickets);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Manage()
        {
            var cinemas = await this.cinemaService.IndexGetAllOrderedByLocationAsync();
            return View(cinemas);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult SetAvailableTickets(Guid cinemaId, Guid movieId)
        {
            var viewModel = new SetAvailableTicketsViewModel
            {
                CinemaId = cinemaId,
                MovieId = movieId
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> SetAvailableTickets(SetAvailableTicketsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = await this.ticketService.SetAvailableTicketsAsync(model);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Unable to set available tickets. Please try again.");
                return View(model);
            }

            return RedirectToAction(nameof(Manage));
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult BuyTicket(Guid cinemaId, Guid movieId)
        {
            string userId = this.User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new BuyTicketViewModel
            {
                CinemaId = cinemaId,
                MovieId = movieId,
                UserId = Guid.Parse(userId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> BuyTicket(BuyTicketViewModel model)
        {
            string userId = this.User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Guid userGuid = Guid.Parse(userId);
            bool result = await this.ticketService.BuyTicketAsync(model, userGuid);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Unable to buy ticket. Please try again.");
                return View(model);
            }

            return RedirectToAction(nameof(MyTickets));
        }
    }
}
