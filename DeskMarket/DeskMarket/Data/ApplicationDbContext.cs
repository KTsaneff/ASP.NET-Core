using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            //builder
            //    .Entity<Category>()
            //    .HasData(
            //        new Category { Name = "Laptops" },
            //        new Category { Name = "Workstations" },
            //        new Category { Name = "Accessories" },
            //        new Category { Name = "Desktops" },
            //        new Category { Name = "Monitors" });
        }
    }
}
