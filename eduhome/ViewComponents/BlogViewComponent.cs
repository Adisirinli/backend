using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public BlogViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = _dbContext.Blogs.ToList();
            return View(await Task.FromResult(blogs));
        }
    }
}
