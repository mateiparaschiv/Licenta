using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Route("Genres/{name?}")]
        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var genreList = await _genreService.GetAsync();
                genreList.Sort((x, y) => string.Compare(x.Name, y.Name));
                return View("~/Views/Genres/Index.cshtml", genreList);
            }
            else
            {
                var genre = await _genreService.GetAsyncByName(name);
                return View("~/Views/Genres/Genre.cshtml", genre);
            }
        }
    }
}
