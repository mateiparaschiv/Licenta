using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        [Route("Albums/{name?}")]
        [Route("Albums/{sortOrder:regex(name_asc|name_desc)?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View("~/Views/Albums/Index.cshtml", await _albumRepository.IndexAlbumList(name, sortOrder));
            }
            else
            {
                return View("~/Views/Albums/Album.cshtml", await _albumRepository.IndexAlbumName(name));
            }
            //TODO : E OK ASA ?
        }
    }
}
