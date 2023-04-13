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

        [Route("Albums/{name?}")]
        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var albumList = await _albumService.GetAsync();
                albumList.Sort((x, y) => string.Compare(x.Name, y.Name));
                return View("~/Views/Albums/Index.cshtml", albumList);
            }
            else
            {
                var album = await _albumService.GetAsyncByName(name);
                return View("~/Views/Albums/Album.cshtml", album);
            }
        }
    }
}
