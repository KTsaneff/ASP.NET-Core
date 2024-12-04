namespace CinemaApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class ManagerService : BaseService, IManagerService
    {
        private readonly IRepository<Manager, Guid> managersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ManagerService(IRepository<Manager, Guid> managersRepository, UserManager<ApplicationUser> userManager)
        {
            this.managersRepository = managersRepository;
            this.userManager = userManager;
        }

        public async Task<bool> IsUserManagerAsync(string? userId)
        {
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                return false;
            }

            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userGuid);
            if (user == null) return false;

            return await this.userManager.IsInRoleAsync(user, "Manager");
        }
    }
}
