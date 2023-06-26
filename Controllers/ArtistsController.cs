using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        public ArtistsController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IActionResult> Index(string? sortOrder, int page)
        {
            return View(await _artistRepository.IndexArtistList(sortOrder, page));
        }

        public async Task<IActionResult> Artist(string name, string? sortOrder)
        {
            return View(await _artistRepository.ArtistName(name, sortOrder));
        }
    }
}
