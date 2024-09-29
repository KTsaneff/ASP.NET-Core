using Microsoft.AspNetCore.Mvc;
using WebApp_Development_dotNET_Eight.Models.Repositories;

namespace WebApp_Development_dotNET_Eight.Controllers
{
    public class ShirtsController : Controller
    {
        public IActionResult Index()
        {
            return View(ShirtRepository.GetShirts());
        }
    }
}
