using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [Route("Artists/{name:alpha}/{sortOrder:regex(name_asc|name_desc|year_asc|year_desc)?}/{pageNumber:int?}")]
        [Route("Artists/{name:alpha}/{pageNumber:int?}")]
        [Route("Artists/{sortOrder:regex(name_asc|name_desc|year_asc|year_desc)?}/{pageNumber:int?}")]
        [Route("Artists/{pageNumber:int?}")]
        public async Task<IActionResult> Index(string? name, string? sortOrder, int pageNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View("~/Views/Artists/Index.cshtml", await _artistRepository.IndexArtistList(name, sortOrder, pageNumber));
            }
            else
            {
                return View("~/Views/Artists/Artist.cshtml", await _artistRepository.IndexArtistName(name, sortOrder));
            }
        }
    }
}
