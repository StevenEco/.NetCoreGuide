using HttpIdentity.Attributes;
using HttpIdentity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HttpIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpBasicAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult BALogout()
        {
            HttpContext.Request.Headers.Authorization = string.Empty;
            HttpContext.Request.Headers.WWWAuthenticate = string.Empty;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}