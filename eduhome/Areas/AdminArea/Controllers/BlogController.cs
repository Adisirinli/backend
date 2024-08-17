using BackEndProject_Edu.Areas.AdminArea.ViewModels.BlogVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Services.Interfaces;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly EduDbContext _dbContext;
        private readonly IEmailService _emailService;


        public BlogController(EduDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;

        }


        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _dbContext.Blogs
                .AsNoTracking()
                .Select(m => new BlogListVM()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImgUrl = m.ImgUrl,
                    Date = m.Date,
                });

            return View(await PaginationVM<BlogListVM>.CreateVM(query, page, 2));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var query = await _dbContext.Blogs
                .AsNoTracking()
                .Select(m => new BlogDetailVM()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Desc = m.Desc,
                    Photo = m.ImgUrl,
                    Date = m.Date,
                }).FirstOrDefaultAsync(m => m.Id == id);

            return View(query);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var blog = await _dbContext.Blogs.AsNoTracking().FirstOrDefaultAsync(k => k.Id == id);
            if (blog is null) return NotFound();
            _dbContext.Remove(blog);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var blog = await _dbContext.Blogs
                .Select(m => new BlogDetailVM()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Desc = m.Desc,
                    Photo = m.ImgUrl,
                }).FirstOrDefaultAsync(p => p.Id == id);


            if (blog is null) return NotFound();
            return View(blog);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM request)
        {
            if (id == null) return BadRequest();
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);
            var file = request.Photo;


            if (blog is null) return NotFound();
            if (!ModelState.IsValid) return View(request);

            blog.Name = request.Name;
            blog.Desc = request.Desc;
            blog.ImgUrl = await SaveFilesAsync(file);


            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);
            var file = request.Photo;
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("Photo", "Image cannot be empty!");
                return View(request);
            }

            Blog blog = new()
            {
                Name = request.Name,
                Desc = request.Desc,
                ImgUrl = await SaveFilesAsync(file)
            };
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();


            List<string> emails = new() { "tahir.aslanlee@gmail.com" };
            string body = $"<a href='http://localhost:5225/blog/detail/{blog.Id}'>Go to blog post</a>";
            _emailService.SendEmail(emails, body, "New Blog Post", "View blog post");

            return RedirectToAction(nameof(Index));
        }



        public async Task<string> SaveFilesAsync(IFormFile file)
        {
            var filePath = Path.Combine("wwwroot/img/blog/", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{file.FileName}";
        }
    }
}
