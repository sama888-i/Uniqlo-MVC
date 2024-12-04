using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uniqlo2.Models;
using Uniqlo2.ViewModels.Auths;

namespace Uniqlo2.Controllers
{
    public class AccountController(UserManager<User> _userManager ) : Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {  if (!ModelState.IsValid)
                return View(vm);
            User user = new User
            {
                Email=vm.Email ,
                FullName =vm.Fullname ,
                UserName =vm.Username ,
                ImageUrl="photo.jpg" //@*ProfilImageUrl*@

            };
            var result = await _userManager.CreateAsync(user,vm.Password );
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description );
                }
                return View();
            }
            return View();

        }
    }
}
