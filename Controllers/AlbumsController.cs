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
        public async Task<IActionResult> Index(string sortOrder, int page)
        {
            return View(await _albumService.IndexAlbumList(sortOrder, page));
        }
        public async Task<IActionResult> Album(string name)
        {
            return View(await _albumService.AlbumName(name));
        }
        public async Task<IActionResult> Year(int year, string sortOrder, int page)
        {
            return View(await _albumService.AlbumsYear(year, sortOrder, page));
        }
        public async Task<IActionResult> Genre(string genre, string sortOrder, int page)
        {
            return View(await _albumService.AlbumsGenre(genre, sortOrder, page));
        }
        public async Task<IActionResult> Sentiment(string sentiment, string sortOrder, int page)
        {
            return View(await _albumService.AlbumsSentiment(sentiment, sortOrder, page));
        }
    }
}