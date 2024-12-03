using CinemaApp.Services.Data.Interfaces;
using CinemaApp.Web.Infrastructure.Extensions;
using CinemaApp.Web.ViewModels.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;

        public TicketApiController(
            ITicketService ticketService,
            ICinemaService cinemaService,
            IManagerService managerService)
        {
            this.ticketService = ticketService;
            this.cinemaService = cinemaService;
            this.managerService = managerService;
        }

        private async Task<bool> IsUserManagerAsync()
        {
            var userId = this.User.GetUserId();
            return !string.IsNullOrWhiteSpace(userId) && await this.managerService.IsUserManagerAsync(userId);
        }

        [HttpGet("GetMoviesByCinema/{cinemaId}")]
        public async Task<IActionResult> GetMoviesByCinema(Guid cinemaId)
        {
            if (!await IsUserManagerAsync())
            {
                return Unauthorized("Only managers can access this endpoint.");
            }

            var cinemaProgram = await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            if (cinemaProgram == null)
            {
                return NotFound("Cinema not found.");
            }

            return Ok(cinemaProgram.Movies);
        }


        [HttpPost("UpdateAvailableTickets")]
        public async Task<IActionResult> UpdateAvailableTickets([FromBody] SetAvailableTicketsViewModel model)
        {
            if (!await IsUserManagerAsync())
            {
                return Unauthorized("Only managers can access this endpoint.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.ticketService.SetAvailableTicketsAsync(model);
            if (!result)
            {
                return BadRequest("Failed to update available tickets. Please try again.");
            }

            return Ok("Ticket availability updated successfully.");
        }

    }
}
