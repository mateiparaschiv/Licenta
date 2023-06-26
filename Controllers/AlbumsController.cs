using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly Interfaces.IRepository.IAlbumService _albumRepository;

        public AlbumsController(Interfaces.IRepository.IAlbumService albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public async Task<IActionResult> Index(string sortOrder)
        {
            return View(await _albumRepository.IndexAlbumList(sortOrder));
        }
        public async Task<IActionResult> Album(string name)
        {
            return View(await _albumRepository.AlbumName(name));
        }
    }
}
