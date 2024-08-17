using Microsoft.AspNetCore.Mvc;

namespace eduhome.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid) return View(request);
            AppUser user = new()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(request);
            }



            await _userManager.AddToRoleAsync(user, RolesEnum.Member.ToString());
            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid) return View(request);
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError("", "Username or Email is wrong...");
                    return View(request);

                }
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (result.IsLockedOut)
            {
                //todo:add lockout minutes
                ModelState.AddModelError("", "Account is Locked for a while ");
                return View(request);
            }
            if (user.IsBlocked)
            {
                ModelState.AddModelError("", "Account is Blocked for a while ");
                return View(request);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View(request);
            }


            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("admin")) return RedirectToAction("index", "dashboard", new { area = "adminarea" });

            return RedirectToAction("index", "home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                ModelState.AddModelError("", "Given email does not exist");
                return View();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = Url.Action(nameof(ResetPassword), "Account"
                , new { email = user.Email, token = token }
                , Request.Scheme
                , Request.Host.ToString());


            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/templates/passwordTemplate/forgetPassword.html"))
            {
                body = reader.ReadToEnd();
            };
            body = body.Replace("{{link}}", url);
            body = body.Replace("{{username}}", user.FullName);

            _emailService.SendEmail(new() { user.Email }, body, "Forget password", "Reset password");

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser is null) return NotFound();
            bool result = await _userManager
                .VerifyUserTokenAsync(existUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (result is false) return Content("Token expired");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, ResetPasswordVM request)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if (!ModelState.IsValid) return View();

            await _userManager.ResetPasswordAsync(appUser, token, request.Password);
            await _userManager.UpdateSecurityStampAsync(appUser);
            await _signInManager.SignInAsync(appUser, true);

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user is null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("index", "home");

        }

        public async Task<IActionResult> AddRole()
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            if (!await _roleManager.RoleExistsAsync("member"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "member" });
            if (!await _roleManager.RoleExistsAsync("superadmin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "superadmin" });
            return Content("Roles added");

        }
    }
}
