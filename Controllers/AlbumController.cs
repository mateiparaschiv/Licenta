
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

        [Route("Albums/{name?}")] /*Index/*/
        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var albumList = await _albumService.GetAsync();
                //var numOfAlbums = _albumService.GetNumOfAlbumsByName(name);
                return View("~/Views/Albums/Index.cshtml", albumList);
            }
            else
            {
                var album = await _albumService.GetAsyncByName(name);
                //return View("~/Views/Artist/Artist.cshtml", artist);
                return View("~/Views/Albums/Album.cshtml", album);
            }

        }
    }
}
