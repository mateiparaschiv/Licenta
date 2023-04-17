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
        [Route("Albums/{sortOrder:regex(name_asc|name_desc)?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
                var albumList = await _albumService.GetAsync();
                _albumService.Shuffle(albumList);
                switch (sortOrder)
                {
                    case "name_asc":
                        albumList.Sort((x, y) => string.Compare(x.Name, y.Name));
                        break;
                    case "name_desc":
                        albumList.Sort((x, y) => string.Compare(y.Name, x.Name));
                        break;
                    case "":
                        break;
                }
                var tuple = new Tuple<List<AlbumModel>, string>(albumList, sortOrder);
                return View("~/Views/Albums/Index.cshtml", tuple);
            }
            else
            {
                var album = await _albumService.GetAsyncByName(name);
                return View("~/Views/Albums/Album.cshtml", album);
            }
        }
    }
}
