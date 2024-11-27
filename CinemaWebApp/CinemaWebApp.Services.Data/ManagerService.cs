using Microsoft.EntityFrameworkCore;

using CinemaWebApp.Data.Models;
using CinemaWebApp.Data.Repository.Interfaces;
using CinemaWebApp.Services.Data.Interfaces;

namespace CinemaWebApp.Services.Data
{
    public class ManagerService : BaseService, IManagerService
    {
        private readonly IRepository<Manager, Guid> managerRepository;

        public ManagerService(IRepository<Manager, Guid> managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public async Task<bool> IsUserManagerAsync(string? userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            bool result = await this.managerRepository
                .GetAllAttached()
                .AnyAsync(m => m.UserId.ToString().ToLower() == userId);

            return result;
        }
    }
}
