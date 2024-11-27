namespace CinemaWebApp.Controllers
{
    using CinemaWebApp.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Extensions;

    public class BaseController : Controller
    {
        protected readonly IManagerService managerService;

        public BaseController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }

        protected async Task<bool> IsUserManagerAsync()
        {
            string? userId = this.User.GetUserId();
            bool isManager = await this.managerService.IsUserManagerAsync(userId);

            return isManager;
        }
    }
}
