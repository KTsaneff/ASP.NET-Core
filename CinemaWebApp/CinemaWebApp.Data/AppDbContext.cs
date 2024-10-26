using CinemaWebApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CinemaWebApp.Models.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<UserMovie> UsersMovies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<CinemaMovie>()
                .HasKey(cm => new { cm.CinemaId, cm.MovieId });

            modelBuilder.Entity<CinemaMovie>()
                .HasOne(cm => cm.Cinema)            
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId);

            modelBuilder.Entity<CinemaMovie>()
                .HasOne(cm => cm.Movie)          
                .WithMany(m => m.CinemaMovies)   
                .HasForeignKey(cm => cm.MovieId);

            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId);

            modelBuilder.Entity<UserMovie>()
                .HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Cinema)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CinemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Movie)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
