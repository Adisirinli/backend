using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.Controllers
{
    public class TeacherController : Controller
    {
        private readonly EduDbContext _dbContext;

        public TeacherController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return BadRequest();
            var teacher = _dbContext.Teachers.FirstOrDefault(b => b.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
    }
}
