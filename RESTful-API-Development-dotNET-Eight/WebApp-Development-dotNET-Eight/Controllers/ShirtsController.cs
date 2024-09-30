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
                try
                {
                    var response = await webApiExecutor.InvokePost("shirts", shirt);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch(WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
            }

            return View(shirt);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int shirtId)
        {
            try
            {
                var shirt = await webApiExecutor.InvokeGet<Shirt>($"shirts/{shirtId}");

                if (shirt != null)
                {
                    return View(shirt);
                }
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View();
            }
            

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(Shirt shirt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await webApiExecutor.InvokePut($"shirts/{shirt.ShirtId}", shirt);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
            }

            return View(shirt);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int shirtId)
        {
            try
            {
                await webApiExecutor.InvokeDelete($"shirts/{shirtId}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index), 
                            await webApiExecutor.InvokeGet<List<Shirt>>("shirts"));
            }
        }

        private void HandleWebApiException(WebApiException ex)
        {
            if (ex.ErrorResponse != null &&
                       ex.ErrorResponse.Errors != null &&
                       ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
                }
            }
        }
    }
}
