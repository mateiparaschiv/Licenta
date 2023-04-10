using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _userService.GetAsync();
            return View(userList);
        }
    }
}
