using Comany_Managment_System_MVC_.Models;
using Comany_Managment_System_MVC_.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Comany_Managment_System_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserVM newUserVM)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.FindByNameAsync(newUserVM.UserName) is not null)
                    ModelState.AddModelError("", "This UserName is Already Exists !!");

                if (_userManager.FindByEmailAsync(newUserVM.Email) is not null)
                    ModelState.AddModelError("", "This Email is Already Exists !!");

                ApplicationUser userModel = new()
                {
                    UserName = newUserVM.UserName,
                    PasswordHash = newUserVM.Password
                };

                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction(nameof(Login));
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);
            }
            return View(newUserVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? userModel = await _userManager.FindByEmailAsync(userVM.Email);

                if (userModel is not null)
                {
                    bool find = await _userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (find == true)
                    {
                        List<Claim> claims = new List<Claim>(); 
                        await _signInManager.SignInWithClaimsAsync(userModel,
                            userVM.RememberMe, claims);

                        return RedirectToAction(nameof(Index), nameof(HomeController));
                    }
                }
                ModelState.AddModelError("", "User and Password is invalid");
            }
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
