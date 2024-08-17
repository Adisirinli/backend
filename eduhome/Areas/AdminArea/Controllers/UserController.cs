using BackEndProject_Edu.Areas.AdminArea.ViewModels.UserVMs;
using BackEndProject_Edu.Hubs;
using BackEndProject_Edu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    //[Authorize(Roles = "SuperAdmin,User")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHubContext<ChatHub> _hubContext;


        public UserController(UserManager<AppUser> userManager, IHubContext<ChatHub> hubContext, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _hubContext = hubContext;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var users = string.IsNullOrEmpty(searchText) ? await _userManager.Users.ToListAsync()
                   : await _userManager.Users.Where(u => u.UserName.ToLower().Contains(searchText.ToLower()) ||
                u.FullName.ToLower().Contains(searchText.ToLower())).ToListAsync();

            return View(users);
        }
        public async Task<IActionResult> ChangeStatus(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return BadRequest();

            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null) return BadRequest();
            var role = await _userManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            await _userManager.DeleteAsync(role);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> SendMessage(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _hubContext.Clients.Client(user.ConnectionId).SendAsync("OrderAccepted", user.Id);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> Update(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();


            UserUpdateVM newUser = new()
            {
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
            };

            return View(newUser);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string id, UserUpdateVM request)
        {
            if (id is null) return BadRequest();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existUser == null) return NotFound();
            if (!ModelState.IsValid) return View(request);

            existUser.FullName = request.FullName;
            existUser.UserName = request.UserName;
            existUser.Email = request.Email;

            await _userManager.UpdateAsync(existUser);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(UserPasswordVM request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return BadRequest();

            var changePassword = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!changePassword.Succeeded)
            {
                foreach (var error in changePassword.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(request);
            }
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index", "User");
        }



    }
}
