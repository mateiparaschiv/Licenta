using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly Interfaces.IRepository.IGenreService _genreService;

        public GenresController(Interfaces.IRepository.IGenreService genreRepository)
        {
            _genreService = genreRepository;
        }

        public async Task<IActionResult> Index(string? sortOrder, int page)
        {
            return View(await _genreService.IndexGenreList(sortOrder, page));
        }
        public async Task<IActionResult> Genre(string name)
        {
            return View(await _genreService.GenreName(name));
        }
    }
}
