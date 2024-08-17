using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public TeacherViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = await _dbContext.Teachers.AsNoTracking().ToListAsync();
            return View(await Task.FromResult(teachers));
        }
    }
}
