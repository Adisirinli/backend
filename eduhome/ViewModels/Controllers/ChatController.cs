using BackEndProject_Edu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Chat()
        {
            var existUser = User.Identity.Name;
            ViewBag.ExistUser = existUser;

            ViewBag.Users = _userManager.Users.ToList();

            return View();
        }
    }
}
