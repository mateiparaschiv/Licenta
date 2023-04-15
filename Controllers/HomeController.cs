using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LicentaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumService _albumService;


        public HomeController(ILogger<HomeController> logger, IAlbumService albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }

        public async Task<IActionResult> Index()
        {
            var albumList = await _albumService.GetAsync();
            return View(albumList);
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