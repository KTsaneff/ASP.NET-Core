using CinemaWebApp.Models.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWebApp.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            AppDbContext dbContext = serviceScope
                .ServiceProvider
                .GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            return app;
        }
    }
}
