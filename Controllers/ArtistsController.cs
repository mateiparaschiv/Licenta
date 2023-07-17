using Microsoft.AspNetCore.Mvc;
namespace LicentaApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;
        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        public async Task<IActionResult> Index(string sortOrder, int page)
        {
            return View(await _artistService.IndexArtistList(sortOrder, page));
        }
        public async Task<IActionResult> Artist(string name, string sortOrder)
        {
            return View(await _artistService.ArtistsName(name, sortOrder));
        }
        public async Task<IActionResult> Sentiment(string sentiment, string sortOrder, int page)
        {
            return View(await _artistService.ArtistsSentiment(sentiment, sortOrder, page));
        }
        public async Task<IActionResult> Formation(bool band, string sortOrder, int page)
        {
            return View(await _artistService.ArtistsFormation(band, sortOrder, page));
        }
    }
}