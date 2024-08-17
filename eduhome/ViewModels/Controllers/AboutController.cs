using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class AboutController : Controller
    {
        private readonly EduDbContext _eduDbContext;

        public AboutController(EduDbContext eduDbContext)
        {
            _eduDbContext = eduDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _eduDbContext.Settings.ToDictionaryAsync(k => k.Key, k => k.Value);
            var teacher = await _eduDbContext.Teachers.ToListAsync();
            var ev = await _eduDbContext.Events.ToListAsync();
            AboutVM aboutVM = new()
            {
                KeyValues = settings,
                Teachers = teacher,
                Events = ev
            };

            return View(aboutVM);
        }

    }
}
