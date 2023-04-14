using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IAlbumService _albumService;

        public GenreController(IGenreService genreService, IAlbumService albumService)
        {
            _genreService = genreService;
            _albumService = albumService;
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
                var genreAlbums = await _albumService.GetAsyncListByGenre(name);
                var tuple = new Tuple<GenreModel, List<AlbumModel>>(genre, genreAlbums);
                return View("~/Views/Genres/Genre.cshtml", tuple);
            }
        }
    }
}
