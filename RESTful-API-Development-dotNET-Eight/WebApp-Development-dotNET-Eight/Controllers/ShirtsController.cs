using Microsoft.AspNetCore.Mvc;
using WebApp_Development_dotNET_Eight.Data;
using WebApp_Development_dotNET_Eight.Models;
using WebApp_Development_dotNET_Eight.Models.Repositories;

namespace WebApp_Development_dotNET_Eight.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly IWebApiExecutor webApiExecutor;

        public ShirtsController(IWebApiExecutor webApiExecuter)
        {
            this.webApiExecutor = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecutor.InvokeGet<List<Shirt>>("shirts"));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                var response = await webApiExecutor.InvokePost("shirts", shirt);
                if(response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(shirt);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int shirtId)
        {
            var shirt = await webApiExecutor.InvokeGet<Shirt>($"shirts/{shirtId}");

            if (shirt != null)
            {
                return View(shirt);
            }

            return NotFound();
        }
    }
}
