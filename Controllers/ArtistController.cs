using ContosoUniversity;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly IReviewService _reviewService;

        public ArtistController(IArtistService artistService, IAlbumService albumService, IReviewService reviewService)
        {
            _artistService = artistService;
            _albumService = albumService;
            _reviewService = reviewService;
        }

        [Route("Artists/{name:alpha}")]
        [Route("Artists/{sortOrder:regex(name_asc|name_desc|year_asc|year_desc)?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder, int? pageNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
                int pageSize = 9;
                var artistList = await _artistService.GetAsync();
                //.AsNoTracking()
                var paginatedArtistList = await PaginatedList<ArtistModel>.CreateAsync(artistList, pageNumber ?? 1, pageSize);
                _artistService.Shuffle(artistList);
                switch (sortOrder)
                {
                    case "name_asc":
                        artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                        break;
                    case "name_desc":
                        artistList.Sort((x, y) => string.Compare(y.Name, x.Name));
                        break;
                    case "":
                        break;
                }

                var albumsToArtist = await _albumService.GetNumOfAlbumsByNames(artistList);
                var tuple = new Tuple<PaginatedList<ArtistModel>, Dictionary<string, int>, string>(paginatedArtistList, albumsToArtist, sortOrder);

                return View("~/Views/Artists/Index.cshtml", tuple);
            }
            else
            {
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
                var artist = await _artistService.GetAsyncByName(name);
                var artistAlbums = await _albumService.GetAsyncListByName(name);
                var reviews = await _reviewService.GetAsyncListByAlbum(name);
                artistAlbums.Sort((x, y) => y.Year - x.Year);

                switch (sortOrder)
                {
                    case "year_asc":
                        artistAlbums.Sort((x, y) => x.Year - y.Year);
                        break;
                    case "year_desc":
                        artistAlbums.Sort((x, y) => y.Year - x.Year);
                        break;
                    case "":
                        break;
                }
                var tuple = new Tuple<ArtistModel, List<AlbumModel>, string, List<ReviewModel>>(artist, artistAlbums, sortOrder, reviews);
                return View("~/Views/Artists/Artist.cshtml", tuple);
            }

        }
        //public async Task<IActionResult> Index(string name, string sortOrder)
        //{

        //}

        //{controller=Home}/{action=Index}/{id?}
        //[Route("Artist/Artist/{name?}")]
        //public async Task<IActionResult> Artist(string? name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }
        //    var artist = await _artistService.GetAsyncByName(name);
        //    return View(artist);
        //}
    }
}
