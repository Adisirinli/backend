using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class SettingFooterViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public SettingFooterViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = _dbContext.Settings.ToDictionary(k => k.Key, k => k.Value);
            return View(await Task.FromResult(setting));
        }
    }
}
