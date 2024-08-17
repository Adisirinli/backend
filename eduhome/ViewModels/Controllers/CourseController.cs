using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class CourseController : Controller
    {
        private readonly EduDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CourseController(EduDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.CourseCounts = _context.Courses.Count();

            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return BadRequest();

            var course = _context.Courses
              .Include(c => c.Category)
              .Include(m => m.CourseTags).ThenInclude(m => m.Tag)
              .Include(m => m.CourseFeatures).ThenInclude(m => m.Features)
              .Include(m => m.Comments).ThenInclude(m => m.AppUser)
              .Include(m => m.BasketCourses)
              .AsNoTracking()
              .FirstOrDefault(b => b.Id == id);

            var blogs = _context.Blogs.AsNoTracking().ToList();
            var categories = _context.Categories.Include(m => m.Course).AsNoTracking().ToList();

            var userId = User.Identity?.Name;
            var basketCourse = _context.BasketCOurses
                .FirstOrDefault(bc => bc.CourseId == id && bc.Basket.AppUserId == userId);

            CourseVM courseVM = new()
            {
                Id = course.Id,
                Name = course.Name,
                ImgUrl = course.ImgUrl,
                Desc = course.Desc,
                About = course.About,
                Apply = course.Apply,
                Categories = categories,
                Certification = course.Certification,
                Blogs = blogs,
                CourseTags = course.CourseTags,
                CourseFeatures = course.CourseFeatures,
                Comments = course.Comments,
                IsAcquired = basketCourse != null && basketCourse.Quantity == 1,
                // CourseCount = basketCourse?.Quantity
            };
            return View(courseVM);
        }
        public async Task<IActionResult> CourseSearch(string text)
        {
            var data = await _context.Courses.Where(k => k.Name.ToLower().Contains(text.ToLower())).Take(5).ToListAsync();
            return PartialView("_CourseSearchPartialView", data);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int courseId, string message)
        {
            AppUser existUser = new();
            if (User.Identity.IsAuthenticated)
            {
                existUser = await _userManager.GetUserAsync(User);
            }
            else
            {
                return RedirectToAction("login", "Account");

            }
            Comment newComment = new()
            {
                Message = message,
                CourseId = courseId,
                AppUserId = existUser.Id
            };
            await _context.Comments.AddAsync(newComment);

            await _context.SaveChangesAsync();
            return Ok();
        }

        public IActionResult LoadMore(int offset = 3)
        {
            var datas = _context.Courses.Skip(offset).Take(3).ToList();
            return PartialView("_CoursePartialView", datas);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public async Task<IActionResult> AddComment(string message, int courseId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (string.IsNullOrEmpty(message)) return NotFound();

            Comment newMessage = new()
            {
                Message = message,
                CourseId = courseId,
                AppUserId = user.Id,
                CreatedDate = DateTime.UtcNow
            };
            _context.Comments.Add(newMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { Id = courseId });
        }
        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (id is null) return BadRequest();

            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("detail", new { id = comment.CourseId });
        }
    }
}
