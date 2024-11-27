using CinemaWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CinemaWebApp.Models.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaMovie> CinemasMovies { get; set; }
        public virtual DbSet<ApplicationUserMovie> UsersMovies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
