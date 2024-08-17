using BackEndProject_Edu.Areas.AdminArea.ViewModels.SpeakerVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SpeakerController : Controller
    {
        private readonly EduDbContext _dbContext;

        public SpeakerController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _dbContext.Speakers
                .Include(s => s.Event)
                .AsNoTracking()
                .Select(s => new SpeakerListVM()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.ImgUrl,
                    Position = s.Position,

                });
            return View(await PaginationVM<SpeakerListVM>.CreateVM(query, page, 2));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var speaker = await _dbContext.Speakers
       .Include(s => s.Event)
       .AsNoTracking()
       .FirstOrDefaultAsync(s => s.Id == id);

            if (speaker == null) return NotFound();


            SpeakerDetailVM vm = new()
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Position = speaker.Position,
                ImgUrl = speaker.ImgUrl,
                EventName = speaker.Event?.Name
            };

            return View(vm);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            var speaker = await _dbContext.Speakers
                .Include(s => s.Event)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (speaker == null) return NotFound();


            SpeakerUpdateVM vm = new()
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Position = speaker.Position,
                Image = speaker.ImgUrl,
                EventId = speaker.Event.Id,

            };

            ViewBag.Events = await GetEvents();

            return View(vm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, SpeakerUpdateVM request)
        {
            if (id is null) return BadRequest();
            var speaker = await _dbContext.Speakers
                .Include(s => s.Event)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (speaker == null) return NotFound();
            var file = request.Photo;

            speaker.Id = request.Id;
            speaker.Name = request.Name;
            speaker.Position = request.Position;
            speaker.ImgUrl = await SaveFilesAsync(file);
            speaker.EventId = request.EventId;

            ViewBag.Events = await GetEvents();

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }














        public async Task<SelectList> GetEvents()
        {
            IEnumerable<Event> events = await _dbContext.Events.ToListAsync();
            return new SelectList(events, "Id", "Name");
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
