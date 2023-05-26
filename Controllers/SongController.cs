using LicentaApp.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
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
