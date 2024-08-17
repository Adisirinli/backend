using BackEndProject_Edu.Data;
using BackEndProject_Edu.Services.Interfaces;
using BackEndProject_Edu.ViewModels;
using Newtonsoft.Json;

namespace BackEndProject_Edu.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly EduDbContext _dbContext;

        public BasketService(IHttpContextAccessor contextAccessor, EduDbContext dbContext)
        {
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }

        public int GetBasketCount() => GetBasketCountFromCookies().Count();

        public List<BasketVM> GetBasketList()
        {
            var list = GetBasketCountFromCookies();
            foreach (var basketProduct in list)
            {
                var existProduct = _dbContext.Courses.FirstOrDefault(p => p.Id == basketProduct.Id);
                if (existProduct != null)
                {
                    basketProduct.Name = existProduct.Name;
                    basketProduct.ImageUrl = existProduct.ImgUrl;
                }
            }
            return list;
        }


        private List<BasketVM> GetBasketCountFromCookies()
        {
            List<BasketVM> list = new();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket != null)
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            return list;
        }
    }
}
