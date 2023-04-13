using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;

        public ArtistController(IArtistService artistService, IAlbumService albumService)
        {
            _artistService = artistService;
            _albumService = albumService;
        }

        [Route("Artists/{name?}")]
        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var artistList = await _artistService.GetAsync();
                artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                //var numOfAlbums = _albumService.GetNumOfAlbumsByName(name);
                //ViewBag.numOfAlbums = numOfAlbums;
                ViewBag.artistList = artistList;
                return View("~/Views/Artists/Index.cshtml", ViewBag);
            }
            else
            {
                var artist = await _artistService.GetAsyncByName(name);
                return View("~/Views/Artists/Artist.cshtml", artist);
            }

        }
        //{controller=Home}/{action=Index}/{id?}
        //[Route("Artist/Artist/{name?}")]
        //public async Task<IActionResult> Artist(string? name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }
        //    var artist = await _artistService.GetAsyncByName(name);
        //    return View(artist);
        //}
    }
}
