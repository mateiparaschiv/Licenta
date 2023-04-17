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
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
                var genreList = await _genreService.GetAsync();
                switch (sortOrder)
                {
                    case "name_asc":
                        genreList.Sort((x, y) => string.Compare(x.Name, y.Name));
                        break;
                    case "name_desc":
                        genreList.Sort((x, y) => string.Compare(y.Name, x.Name));
                        break;
                    case "":
                        break;
                }
                var tuple = new Tuple<List<GenreModel>, string>(genreList, sortOrder);
                return View("~/Views/Genres/Index.cshtml", tuple);
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
