
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Repository.Data;

namespace Comany_Managment_System_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;
        private readonly IEmployeeService _employeeService;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailSettings emailSettings, 
            IEmployeeService employeeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSettings = emailSettings;
            _employeeService = employeeService;
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
                    ModelState.AddModelError(string.Empty, "This UserName is Already Exists !!");

                if (_userManager.FindByEmailAsync(newUserVM.Email) is not null)
                    ModelState.AddModelError(string.Empty, "This Email is Already Exists !!");

                ApplicationUser userModel = new()
                {
                    UserName = newUserVM.UserName,
                    Email = newUserVM.Email,
                    PasswordHash = newUserVM.Password
                };

                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userModel, newUserVM.Role);
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction(nameof(Login));
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
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
                    bool passwordMatches = await _userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (passwordMatches)
                    {
                        var employee = await _employeeService.Find(e => e.Email.ToLower() == userModel.Email!.ToLower());

                        if (employee is not null)
                        {
                            List<Claim> claims = new List<Claim>
                            {
                                new Claim("EmployeeId", employee!.Id.ToString()),
                            };
                            await _signInManager.SignInWithClaimsAsync(userModel, userVM.RememberMe, claims);
                            return RedirectToAction(nameof(Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "User and Password is invalid");
            }
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passResetLink = Url.Action(nameof(ResetPassword),
                    "Account",
                    new { email = model.Email, token = token },
                    protocol: Request.Scheme);  

                    var email = new EmailVM()
                    {
                        Subject = "Reset Password",
                        To = user.Email!,
                        Body = $"Please reset your password using the following link: <a href='{passResetLink}'>Reset Password</a>"
                    };

                    try
                    {
                        await Task.Run(() => _emailSettings.SendEmail(email)); 
                        return RedirectToAction(nameof(CheckYourInbox));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"There was an issue sending the email. Please try again later {ex.Message}.");
                    }
                }
                ModelState.AddModelError(string.Empty, "Email does not exist.");
            }

            return View(nameof(ForgetPassword), model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email!);
                var result = await _userManager.ResetPasswordAsync(user!, token!, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);
        }
    }
}
