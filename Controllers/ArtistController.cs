﻿using Microsoft.AspNetCore.Mvc;

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

        [Route("Artists/{name:alpha}")]
        [Route("Artists/{sortOrder:regex(name_asc|name_desc)?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            //sortOrder = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            if (string.IsNullOrWhiteSpace(name))
            {
                var artistList = await _artistService.GetAsync();
                //artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                //ViewBag.DateSortParm = sortOrder;
                //TempData["DateSortParm"] = sortOrder;
                switch (TempData["DateSortParm"])
                {
                    case "name_asc":
                        artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                        break;
                    case "name_desc":
                        artistList.Sort((x, y) => string.Compare(y.Name, x.Name));
                        break;
                }

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
