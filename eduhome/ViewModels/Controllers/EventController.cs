using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class EventController : Controller
    {
        private readonly EduDbContext _dbContext;

        public EventController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();
            var item = _dbContext.Events.Include(i => i.Speakers).FirstOrDefault(k => k.Id == id);
            var courses = _dbContext.Courses.AsNoTracking().ToList();
            if (item == null && courses is null) return NotFound();

            EventVM eventVM = new()
            {
                Name = item.Name,
                Desc = item.Desc,
                ImgUrl = item.ImgUrl,
                Time = item.Time,
                Venue = item.Venue,

                Courses = courses,
                Speakers = item.Speakers
            };
            return View(eventVM);
        }
        public async Task<IActionResult> EventSearch(string text)
        {
            var data = await _dbContext.Events.Where(k => k.Name.ToLower().Contains(text.ToLower())).Take(5).ToListAsync();
            return PartialView("_EventSearchPartialView", data);
        }
    }
}
