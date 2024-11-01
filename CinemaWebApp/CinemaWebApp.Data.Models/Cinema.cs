using CinemaWebApp.Models;

namespace CinemaWebApp.Data.Models
{
    public class Cinema
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        // This property is used to mark the cinema as deleted
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
