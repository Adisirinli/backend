using BackEndProject_Edu.Areas.AdminArea.ViewModels.RoleVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly EduDbContext _dbContext;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, EduDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        //public async Task<IActionResult> Detail(string? id)
        //{
        //    if (id == null) return BadRequest();
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role is null) return NotFound();

        //    var users = _userManager.Users.ToListAsync();

        //    var userRolesVM = new List<RoleListVM>();

        //    foreach (var user in await users)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        if (userRoles.Contains(role.Name))
        //        {
        //            userRolesVM.Add(new RoleListVM(user.UserName, userRoles.ToList()));
        //        }
        //    }
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName
                    });

                return RedirectToAction("index");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> UpdateUserRole(string id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleUpdateVm = new RoleUpdateVM(user.UserName, roles, userRoles);
            return View(roleUpdateVm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string id, List<string> newUserRoles)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, newUserRoles);

            return RedirectToAction("index", "user");
        }



    }
}
