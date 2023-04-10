using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ArtistService _artistService;

        public ArtistsController(ArtistService artistService)
        {
            _artistService = artistService;
        }
        public async Task<IActionResult> Index()
        {
            var artistList = await _artistService.GetAsync();
            return View(artistList);
        }
        //{controller=Home}/{action=Index}/{id?}
        [Route("Artists/Artist/{name?}")]
        public async Task<IActionResult> Artist(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }
            var artist = await _artistService.GetAsyncByName(name);
            return View(artist);
        }
    }
}
