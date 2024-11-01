using CinemaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CinemaWebApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<ApplicationUserMovie> ApplicationUserMovies { get; set; } = new List<ApplicationUserMovie>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
