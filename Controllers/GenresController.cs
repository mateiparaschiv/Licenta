using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index(string sortOrder, int page)
        {
            return View(await _genreService.IndexGenreList(sortOrder, page));
        }
        public async Task<IActionResult> Genre(string name)
        {
            return View(await _genreService.GenreName(name));
        }
    }
}
