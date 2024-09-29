using Microsoft.EntityFrameworkCore;
using RESTful_API_Development_dotNET_Eight.Models;

namespace RESTful_API_Development_dotNET_Eight.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //data seeding
            modelBuilder.Entity<Shirt>().HasData
            (
            new Shirt { ShirtId = 1, Brand = "Nike", Color = "Blue", Gender = "Male", Price = 20.00, Size = 10 },
            new Shirt { ShirtId = 2, Brand = "Adidas", Color = "Red", Gender = "Female", Price = 25.00, Size = 7 },
            new Shirt { ShirtId = 3, Brand = "Puma", Color = "Green", Gender = "Male", Price = 30.00, Size = 12 },
            new Shirt { ShirtId = 4, Brand = "Reebok", Color = "White", Gender = "Male", Price = 15.00, Size = 9 },
            new Shirt { ShirtId = 5, Brand = "Under Armour", Color = "Purple", Gender = "Female", Price = 22.00, Size = 6 }
            );
        }
    }
}
