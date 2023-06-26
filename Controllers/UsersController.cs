using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
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
