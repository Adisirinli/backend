using eduhome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eduhome.Controllers
{
    public class ContactController:Controller
    {
        private readonly EduhomeDbContext _context;

        public ContactController(EduhomeDbContext context)
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
