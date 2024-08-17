using eduhome.Data;
using Microsoft.AspNetCore.Mvc;

namespace eduhome.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduhomeDbContext _context;

        public HomeController(EduhomeDbContext context)
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

