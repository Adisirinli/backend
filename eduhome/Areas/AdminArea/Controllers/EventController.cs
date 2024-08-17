using BackEndProject_Edu.Areas.AdminArea.ViewModels.EventVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Services.Interfaces;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EventController : Controller
    {
        private readonly EduDbContext _dbContext;
        private readonly IEmailService _emailService;


        public EventController(EduDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _dbContext.Events
                .Include(e => e.Speakers)
                .AsNoTracking();

            return View(await PaginationVM<Event>.CreateVM(query, page, 2));
        }



        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var query = await _dbContext.Events
                .Include(e => e.Speakers)
                .AsNoTracking()
                .Select(m => new EventDetailVM()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImgUrl = m.ImgUrl,
                    Desc = m.Desc,
                    Time = DateTime.Now,
                    Speakers = m.Speakers
                }).FirstOrDefaultAsync(m => m.Id == id);



            return View(query);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var events = await _dbContext.Events.ToListAsync();
            if (events == null) return BadRequest();
            _dbContext.Remove(events);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(EventCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

            var file = request.ImgUrl;

            Event ev = new()
            {
                Name = request.Name,
                Desc = request.Desc,
                Time = DateTime.Now,
                ImgUrl = await SaveFilesAsync(file)
            };

            await _dbContext.Events.AddAsync(ev);
            await _dbContext.SaveChangesAsync();

            List<string> emails = new() { "tahir.aslanlee@gmail.com" };
            string body = $"<a href='http://localhost:5225/event/detail/{ev.Id}'>Go to Event post</a>";
            _emailService.SendEmail(emails, body, "New Event Post", "View Event post");


            return RedirectToAction("index");
        }






        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            var ev = await _dbContext.Events
                .Select(m => new EventUpdateVM()
                {

                    Id = m.Id,
                    Name = m.Name,
                    Desc = m.Desc,
                    Time = DateTime.Now,
                    Venue = m.Venue,
                    ImgUrl = m.ImgUrl
                }).FirstOrDefaultAsync(e => e.Id == id);

            if (ev is null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, EventUpdateVM request)
        {
            if (id is null) return BadRequest();
            var ev = await _dbContext.Events.FindAsync(id);

            var file = request.Photo;

            ev.Id = request.Id;
            ev.Name = request.Name;
            ev.Desc = request.Desc;
            ev.Time = DateTime.Now;
            ev.Venue = request.Venue;
            ev.ImgUrl = await SaveFilesAsync(file);


            await _dbContext.SaveChangesAsync();
            return RedirectToAction("index");
        }







        public async Task<string> SaveFilesAsync(IFormFile file)
        {
            var filePath = Path.Combine("wwwroot/img/event/", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{file.FileName}";
        }

    }
}
