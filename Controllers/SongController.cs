using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class SongController : Controller
    {
        private readonly SongService _songService;

        public SongController(SongService songService)
        {
            _songService = songService;
        }
        public async Task<IActionResult> Index()
        {
            var songList = await _songService.GetAsync();
            return View(songList);
        }
    }
}
