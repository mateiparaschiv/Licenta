using LicentaApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace LicentaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _homeService.IndexHome());
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
        [Route("/Home/Test", Name = "Custom")]
        public string Test()
        {
            return "This is the test page and indicates work in progress.";
        }
    }
}