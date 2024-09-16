using Microsoft.AspNetCore.Mvc;
using MVCIntroDemo.Models;
using System.Diagnostics;

namespace MVCIntroDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello, World!";
            ViewBag.Paragraph = "Welcome to ASP.NET Core MVC!";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.NET MVC app.";
            return View();
        }

        public IActionResult Numbers()
        {
            ViewBag.Message = "Number in range [1 ... 10]";
            return View();
        }

        public IActionResult Function(int count = 6)
        {
            ViewBag.Message = "Enter number limit...";
            ViewBag.Count = count;
           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
