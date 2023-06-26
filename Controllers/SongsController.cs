using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongRepository _songService;

        public SongsController(ISongRepository songService)
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
