using LicentaApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LicentaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumRepository _albumRepository;


        public HomeController(ILogger<HomeController> logger, IAlbumRepository albumRepository)
        {
            _logger = logger;
            _albumRepository = albumRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _albumRepository.GetRandomAlbums(3));
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