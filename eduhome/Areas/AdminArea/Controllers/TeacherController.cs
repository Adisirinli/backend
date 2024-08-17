using BackEndProject_Edu.Areas.AdminArea.ViewModels.TeacherVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Services.Interfaces;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TeacherController : Controller
    {
        private readonly EduDbContext _dbContext;
        private readonly IEmailService _emailService;

        public TeacherController(EduDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _dbContext.Teachers
                .AsNoTracking()
                .Select(t => new TeacherListVM()
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImgUrl = t.ImgUrl,
                    Position = t.Position,

                });

            return View(await PaginationVM<TeacherListVM>.CreateVM(query, page, 2));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);

            TeacherDetailVM teacherDetail = new()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                ImgUrl = teacher.ImgUrl,
                Position = teacher.Position,
                About = teacher.About,
                Degree = teacher.Degree,
                Experience = teacher.Experience,
                Hobbies = teacher.Hobbies,
                Faculty = teacher.Faculty,
                Phone = teacher.Phone,
                Skype = teacher.Skype,
                Mail = teacher.Mail,
                Language = teacher.Language,
                TeamLeader = teacher.TeamLeader,
                Design = teacher.Design,
                Innovation = teacher.Innovation,
                Communication = teacher.Communication,
            };

            return View(teacherDetail);


        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return BadRequest();

            _dbContext.Remove(teacher);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(TeacherCreateVM request)
        {
            //if (!ModelState.IsValid) return View(request);

            bool isExist = await _dbContext.Teachers.AnyAsync(m => m.Name == request.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", $"{request.Name} Allready exists");
                return View(request);
            }

            var file = request.Photo;

            Teacher teacher = new()
            {
                Name = request.Name,
                ImgUrl = await SaveFilesAsync(file),
                Position = request.Position,
                About = request.About,
                Degree = request.Degree,
                Experience = request.Experience,
                Hobbies = request.Hobbies,
                Faculty = request.Faculty,
                Phone = request.Phone,
                Skype = request.Skype,
                Mail = request.Mail,
                Language = request.Language,
                TeamLeader = request.TeamLeader,
                Development = request.Development,
                Design = request.Design,
                Innovation = request.Innovation,
                Communication = request.Communication
            };

            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            List<string> emails = new() { "tahir.aslanlee@gmail.com" };
            string body = $"<a href='http://localhost:5225/teacher/detail/{teacher.Id}'>Go to teacher post</a>";
            _emailService.SendEmail(emails, body, "New Teacher Post", "View teacher post");


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            var existTeacher = await _dbContext.Teachers.FindAsync(id);
            if (existTeacher == null) return BadRequest();



            TeacherUpdateVM updateVM = new()
            {
                Id = existTeacher.Id,
                ImgUrl = existTeacher.ImgUrl,
                Name = existTeacher.Name,
                Position = existTeacher.Position,
                About = existTeacher.About,
                Degree = existTeacher.Degree,
                Experience = existTeacher.Experience,
                Hobbies = existTeacher.Hobbies,
                Faculty = existTeacher.Faculty,
                Phone = existTeacher.Phone,
                Skype = existTeacher.Skype,
                Mail = existTeacher.Mail,
                Language = existTeacher.Language,
                TeamLeader = existTeacher.TeamLeader,
                Development = existTeacher.Development,
                Design = existTeacher.Design,
                Innovation = existTeacher.Innovation,
                Communication = existTeacher.Communication
            };

            return View(updateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id, TeacherUpdateVM request)
        {
            if (id == null) return BadRequest();

            var teacher = await _dbContext.Teachers.FindAsync(id);
            if (teacher == null) return BadRequest();

            var file = request.Photo;
            teacher.Name = request.Name;
            teacher.ImgUrl = await SaveFilesAsync(file);
            teacher.Position = request.Position;
            teacher.About = request.About;
            teacher.Degree = request.Degree;
            teacher.Experience = request.Experience;
            teacher.Hobbies = request.Hobbies;
            teacher.Faculty = request.Faculty;
            teacher.Phone = request.Phone;
            teacher.Skype = request.Skype;
            teacher.Mail = request.Mail;
            teacher.Language = request.Language;
            teacher.TeamLeader = request.TeamLeader;
            teacher.Development = request.Development;
            teacher.Design = request.Design;
            teacher.Innovation = request.Innovation;
            teacher.Communication = request.Communication;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }


        public async Task<string> SaveFilesAsync(IFormFile file)
        {
            var filePath = Path.Combine("wwwroot/img/teacher/", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{file.FileName}";
        }

    }
}
