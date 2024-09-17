using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet<Movie> Movies will be used to interact with the Movies table
        public DbSet<Movie> Movies { get; set; }
    }
}
