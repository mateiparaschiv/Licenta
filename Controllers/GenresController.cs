using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index(string? sortOrder)
        {
            return View(await _genreRepository.IndexGenreList(sortOrder));
        }
        public async Task<IActionResult> Genre(string name)
        {
            return View(await _genreRepository.GenreName(name));
        }
    }
}
