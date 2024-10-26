using CinemaWebApp.Data.Models;

namespace CinemaWebApp.Models
{
    public class UserMovie
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
