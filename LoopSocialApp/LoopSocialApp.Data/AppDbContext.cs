using LoopSocialApp.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LoopSocialApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {            
        }

        public DbSet<Post> Posts { get; set; } = null!;
    }
}
