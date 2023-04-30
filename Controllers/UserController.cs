using LicentaApp.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
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
