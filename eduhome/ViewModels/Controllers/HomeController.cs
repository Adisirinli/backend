using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduDbContext _context;

        public HomeController(EduDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.AsNoTracking().ToList();
            var events = _context.Events.AsNoTracking().ToList();
            var teachers = _context.Teachers.AsNoTracking().ToList();
            HomeVM homeVM = new()
            {
                Sliders = sliders,
                Events = events,
                Teachers = teachers
            };
            return View(homeVM);
        }

    }
}
