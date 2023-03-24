using LicentaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly LicentaService _licentaService;

        public UserController(LicentaService licentaService)
        {
            _licentaService = licentaService;
        }
        public async Task<IActionResult> Index()
        {

            //var userList = _licentaService.GetAsync();
            var userList = await _licentaService.GetAsync();
            return View(userList);
        }
    }
}
