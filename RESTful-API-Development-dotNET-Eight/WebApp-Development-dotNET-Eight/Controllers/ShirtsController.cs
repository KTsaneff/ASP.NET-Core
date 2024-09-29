using Microsoft.AspNetCore.Mvc;
using WebApp_Development_dotNET_Eight.Data;
using WebApp_Development_dotNET_Eight.Models;
using WebApp_Development_dotNET_Eight.Models.Repositories;

namespace WebApp_Development_dotNET_Eight.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ShirtsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shirt shirt)
        {
            return View(shirt);
        }
    }
}
