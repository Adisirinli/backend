using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Views.Shared.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.ViewComponents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;


        public SettingHeaderViewComponent(EduDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser existUser = new();

            if (User.Identity.IsAuthenticated)
            {
                existUser = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            int basketCount = await _dbContext.BasketCOurses
         .Where(m => m.Basket.AppUserId == existUser.Id)
         .SumAsync(m => m.Quantity);

            decimal totalPrice = (decimal)await _dbContext.BasketCOurses
                .Where(m => m.Basket.AppUserId == existUser.Id)
                .SumAsync(m => m.Course.Price * m.Quantity);

            HeaderVM model = new()
            {
                Settings = _dbContext.Settings.ToDictionary(k => k.Key, k => k.Value),
                BasketCount = basketCount,
                TotalPrice = totalPrice,
            };


            if (User.Identity.IsAuthenticated)
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.FullName = user.FullName;
            }



            return View(await Task.FromResult(model));
        }
    }
}
