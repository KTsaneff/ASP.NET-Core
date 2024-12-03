using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.Tickets
{
    public class BuyTicketRequest
    {
        [Required]
        public Guid CinemaId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select at least one ticket.")]
        public int Quantity { get; set; }
    }
}
