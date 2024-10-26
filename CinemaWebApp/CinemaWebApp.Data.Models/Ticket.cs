using CinemaWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaWebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
