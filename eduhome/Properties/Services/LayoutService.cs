using BackEndProject_Edu.Data;
using BackEndProject_Edu.Services.Interfaces;

namespace BackEndProject_Edu.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly EduDbContext _dbContext;

        public LayoutService(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IDictionary<string, string> GetSettings() => _dbContext.Settings
            .ToDictionary(k => k.Key, k => k.Value);

        //public IActionResult GetTeachers() => _dbContext.Teachers.ToList();

    }
}
