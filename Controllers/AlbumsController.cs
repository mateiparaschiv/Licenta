using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        public async Task<IActionResult> Index(string sortOrder)
        {
            return View(await _albumService.IndexAlbumList(sortOrder));
        }
        public async Task<IActionResult> Album(string name)
        {
            return View(await _albumService.AlbumName(name));
        }
    }
}
