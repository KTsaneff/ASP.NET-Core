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
                if (response != null)
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
        [HttpPost]
        public async Task<IActionResult> Update(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                await webApiExecutor.InvokePut($"shirts/{shirt.ShirtId}", shirt);
                return RedirectToAction(nameof(Index));
            }
            return View(shirt);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int shirtId)
        {
            await webApiExecutor.InvokeDelete($"shirts/{shirtId}");
            return RedirectToAction(nameof(Index));
        }
    }
}
