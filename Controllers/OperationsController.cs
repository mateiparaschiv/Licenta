using LicentaApp.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApp.Controllers
{
    public class OperationsController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public OperationsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, userModel.Password);
                if (result.Succeeded)
                    ViewBag.Message = "User Created Successfully";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(userModel);
        }
    }
}
