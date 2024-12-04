using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CinemaApp.Data.Configuration
{
    public static class DatabaseSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = { "Admin", "Manager", "User" };

            foreach (var role in roles)
            {
                var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole<Guid> { Name = role }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }

        public static void AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
        }

        public static void ImportMovies(IServiceProvider serviceProvider, string jsonFilePath)
        {
            CinemaDbContext cinemaDbContext = serviceProvider.GetRequiredService<CinemaDbContext>();

            if (cinemaDbContext.Movies.Any())
            {
                return;
            }

            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                var movies = JsonSerializer.Deserialize<List<Movie>>(jsonData);

                if (movies == null || !movies.Any())
                {
                    throw new Exception("No movies found in the JSON file.");
                }

                cinemaDbContext.Movies.AddRange(movies);
                cinemaDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing movies: {ex.Message}");
                throw;
            }
        }
    }
}
