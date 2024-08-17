namespace eduhome.Controllers
{
    public class ChatController:Controller
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
}
