using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Models.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaMovie> CinemasMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Make sure to call the base class method

            //Configure the composite key for the CinemaMovie entity
            modelBuilder.Entity<CinemaMovie>()
                .HasKey(cm => new { cm.CinemaId, cm.MovieId });

            //Configure the relationship between Cinema and CinemaMovie
            modelBuilder.Entity<CinemaMovie>()
                .HasOne(cm => cm.Cinema)            //A CinemaMovie entity has one Cinema...
                .WithMany(c => c.CinemaMovies)      //A Cinema entity can have many CinemaMovies
                .HasForeignKey(cm => cm.CinemaId);  //CinemaId is the foreign key in CinemaMovie

            //Configure the relationship between Movie and CinemaMovie
            modelBuilder.Entity<CinemaMovie>()
                .HasOne(cm => cm.Movie)             //A CinemaMovie entity has one Movie...
                .WithMany(m => m.CinemaMovies)      //A Movie entity can have many CinemaMovies
                .HasForeignKey(cm => cm.MovieId);   //MovieId is the foreign key in CinemaMovie
        }
    }
}
