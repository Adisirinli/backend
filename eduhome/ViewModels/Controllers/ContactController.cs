using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class ContactController : Controller
    {
        private readonly EduDbContext _context;

        public ContactController(EduDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var settings = _context.Settings.AsNoTracking().ToDictionary(k => k.Key, k => k.Value);
            return View(settings);
        }
    }
}
