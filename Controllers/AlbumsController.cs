using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumsController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public async Task<IActionResult> Index(string? sortOrder)
        {
            return View(await _albumRepository.IndexAlbumList(sortOrder));
        }
        public async Task<IActionResult> Album(string name)
        {
            return View(await _albumRepository.AlbumName(name));
        }
    }
}
