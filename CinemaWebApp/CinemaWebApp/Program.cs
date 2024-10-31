using CinemaWebApp.Data.Models;
using CinemaWebApp.Infrastructure.Repositories.Contracts;
using CinemaWebApp.Infrastructure.Repositories;
using CinemaWebApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using CinemaWebApp.ViewModels.Error;
using System.Reflection;
using CinemaWebApp.Services.Mapping;

namespace CinemaWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddControllersWithViews();

            // Register repositories in the DI container
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 
            // Register the generic repository
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();           
            // Register specific repositories
            // Repeat for other specific repositories (e.g., ICinemaRepository, ITicketRepository)

            var app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
