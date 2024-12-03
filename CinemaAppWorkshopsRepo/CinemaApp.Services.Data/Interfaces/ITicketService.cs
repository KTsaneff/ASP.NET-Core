using CinemaApp.Web.ViewModels.Tickets;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface ITicketService
    {
        Task<bool> BuyTicketAsync(BuyTicketViewModel model, Guid userId);
        Task<IEnumerable<UserTicketViewModel>> GetUserTicketsAsync(Guid userId);
        Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel model);

        Task<bool> DecreaseAvailableTicketsAsync(Guid cinemaId, Guid movieId, int numberOfTickets);
    }
}
