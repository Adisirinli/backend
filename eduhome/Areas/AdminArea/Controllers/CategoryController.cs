using BackEndProject_Edu.Areas.AdminArea.ViewModels.CategoryVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly EduDbContext _dbContext;

        public CategoryController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _dbContext.Categories
                .AsNoTracking();
            return View(await PaginationVM<Category>.CreateVM(query, page, 2));
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
            var detail = await _dbContext.Categories
                .Include(m => m.Course)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (detail is null) return BadRequest();
            return View(detail);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var details = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
            if (details is null) return BadRequest();
            return View(new CategoryUpdateVM()
            {
                Name = details.Name,

            });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM request)
        {
            {
                if (id == null) return BadRequest();

                if (!ModelState.IsValid) return View(request);

                var details = await _dbContext.Categories
                    .FirstOrDefaultAsync(k => k.Id == id);

                if (details is null) return BadRequest();

                var existCategoryName = _dbContext.Categories
                    .Any(k => k.Name.ToLower() == request.Name.ToLower()
                    && k.Id != id);

                if (existCategoryName)
                {
                    ModelState.AddModelError("Name", "Given name already exists");
                    return View(request);
                }
                details.Name = request.Name;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryListVM request)
        {
            if (!ModelState.IsValid) return View(request);
            if (await _dbContext.Categories.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Cant Create With Same Name");
                return View(request);
            }
            var newCat = new Category()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
            };
            await _dbContext.Categories.AddAsync(newCat);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
