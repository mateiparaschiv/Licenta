using LicentaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;

        //
        public AlbumController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        //split!!!!!!!!!!!!!!!!!!!!!
        [Route("Albums/{name}")]
        [Route("Albums/{sortOrder:regex(name_asc|name_desc)?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View(await _albumRepository.IndexAlbumList(sortOrder));
            }
            else
            {

                return View("~/Views/Albums/Album.cshtml", await _albumRepository.IndexAlbumName(name));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumModel album)
        {
            if (ModelState.IsValid)
            {
                await _albumRepository.CreateAsync(album);
            }
            return View("CreateAlbum", album);
        }
    }
}
