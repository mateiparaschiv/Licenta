using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;

        public ArtistController(IArtistService artistService, IAlbumService albumService)
        {
            _artistService = artistService;
            _albumService = albumService;
        }

        [Route("Artists/{name?}")]
        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                //var artist1 = new ArtistModel("Pink Floyd", "Pink Floyd is a British rock band who managed to carve a path for progressive and psychedelic music in a way that was uniquely fascinating at the time and has remained equally momentous in the modern age. The name “Pink Floyd” came from two blues musicians that founding member Syd Barrett idolized—Pink Anderson and Floyd Council."
                //    , true, null, null, 1963, null);
                //_artistService.CreateAsync(artist1);
                //var artist2 = new ArtistModel("Death Grips", "Death Grips is an experimental multi-genre group from Sacramento, CA, formed in 2010. Their music and live performances consist of vocals from Stefan “MC Ride” Burnett and production from Zach Hill and Andy Morin. They released their first mixtape, Exmilitary on April 25, 2011, with their most popular song, Guillotine, on that project.",
                //    true, null, null, 2010, null);
                //_artistService.CreateAsync(artist2);


                //var artist = new ArtistModel("Jim Morrison", "James Douglas Morrison (December 8, 1943 – July 3, 1971) was an American singer, poet and songwriter who was the lead vocalist of the rock band the Doors. Due to his wild personality, poetic lyrics, distinctive voice, unpredictable and erratic performances, and the dramatic circumstances surrounding his life and early death, Morrison is regarded by music critics and fans as one of the most influential frontmen in rock history."
                //    , false, new DateOnly(1943, 12, 8), new DateOnly(1971, 7, 3), null, null);
                //_artistService.CreateAsync(artist);

                //var artist3 = new ArtistModel("Bladee", "Benjamin Thage Dag Reichwald (born 9 April 1994), professionally known as Bladee, is a Swedish recording artist, graphic designer, and fashion designer from Stockholm.",
                //    false, new DateOnly(1994, 4, 9), null, null, null);
                //_artistService.CreateAsync(artist3);

                var artistList = await _artistService.GetAsync();
                artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                var albumsToArtist = await _albumService.GetNumOfAlbumsByNames(artistList);
                var tuple = new Tuple<List<ArtistModel>, Dictionary<string, int>>(artistList, albumsToArtist);
                return View("~/Views/Artists/Index.cshtml", tuple);
            }
            else
            {
                var artist = await _artistService.GetAsyncByName(name);
                var artistAlbums = await _albumService.GetAsyncListByName(name);
                artistAlbums.Sort((x, y) => x.Year - y.Year);
                var tuple = new Tuple<ArtistModel, List<AlbumModel>>(artist, artistAlbums);
                return View("~/Views/Artists/Artist.cshtml", tuple);
            }

        }
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
