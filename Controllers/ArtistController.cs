using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }
        public async Task<IActionResult> Index()
        {
            var artistList = await _artistService.GetAsync();
            return View(artistList);
        }
    }
}
