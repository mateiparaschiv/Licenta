using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [Route("Genres/{name?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View("~/Views/Genres/Index.cshtml", await _genreRepository.IndexGenreList(name, sortOrder));
            }
            else
            {
                return View("~/Views/Genres/Genre.cshtml", await _genreRepository.IndexGenreName(name));
            }
        }
    }
}
