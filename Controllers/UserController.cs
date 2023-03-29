using LicentaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService licentaService)
        {
            _userService = licentaService;
        }
        public async Task<IActionResult> Index()
        {

            //var userList = _licentaService.GetAsync();
            var userList = await _userService.GetAsync();
            return View(userList);
        }
    }
}
