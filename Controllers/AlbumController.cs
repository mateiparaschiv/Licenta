
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        public async Task<IActionResult> Index()
        {
            var albumList = await _albumService.GetAsync();
            return View(albumList);
        }
    }
}
