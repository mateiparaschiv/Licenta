using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
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
