using Microsoft.AspNetCore.Mvc;

namespace eduhome.Controllers
{
    public class BasketController:Controller
    {

        private readonly EduDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(EduDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            var existUser = await _userManager.GetUserAsync(User);
            if (existUser == null) return NotFound();

            Basket existBasket = await _dbContext.Baskets
                .Include(b => b.BasketCourses)
                .ThenInclude(b => b.Course)
                .FirstOrDefaultAsync(m => m.AppUserId == existUser.Id);

            List<BasketVM> model = new();

            if (existBasket is not null)
            {
                foreach (var item in existBasket.BasketCourses)
                {
                    model.Add(new BasketVM()
                    {
                        Id = item.CourseId,
                        ImageUrl = item.Course.ImgUrl,
                        Name = item.Course.Name,
                        Price = (int)item.Course.Price,
                        TotalPrice = (int)item.Course.Price * item.Quantity,
                        BasketCount = item.Quantity
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int? courseId)
        {
            if (courseId is null) return BadRequest();

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "User is not authenticated", redirectUrl = Url.Action("Login", "Account") });
            }

            AppUser existUser = await _userManager.GetUserAsync(User);
            Course existCourse = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (existCourse is null) return NotFound();

            BasketCourse basketProduct = await _dbContext.BasketCOurses
                .FirstOrDefaultAsync(c => c.CourseId == courseId && c.Basket.AppUserId == existUser.Id);

            Basket existBasket = await _dbContext.Baskets
                .Include(c => c.BasketCourses)
                .FirstOrDefaultAsync(b => b.AppUserId == existUser.Id);

            if (basketProduct is not null)
            {
                basketProduct.Quantity++;
                await _dbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Course added to basket" });
            }

            if (existBasket is not null)
            {
                existBasket.BasketCourses.Add(new BasketCourse
                {
                    Quantity = 1,
                    BasketId = existBasket.Id,
                    CourseId = (int)courseId
                });

                await _dbContext.SaveChangesAsync();
                return Json(new { success = true, message = "Course added to basket" });
            }

            Basket newBasket = new()
            {
                AppUserId = existUser.Id,
            };

            await _dbContext.Baskets.AddAsync(newBasket);
            await _dbContext.SaveChangesAsync();

            BasketCourse newBasketCourse = new()
            {
                Quantity = 1,
                BasketId = newBasket.Id,
                CourseId = (int)courseId
            };

            await _dbContext.BasketCOurses.AddAsync(newBasketCourse);
            await _dbContext.SaveChangesAsync();

            return Json(new { success = true, message = "Course added to basket" });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int? courseId)
        {
            if (courseId is null) return BadRequest();

            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "User is not authenticated", redirectUrl = Url.Action("Login", "Account") });
            }

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);


            BasketCourse basketProduct = await _dbContext.BasketCOurses
                .FirstOrDefaultAsync(c => c.CourseId == courseId && c.Basket.AppUserId == existUser.Id);

            if (basketProduct is null) return NotFound();


            _dbContext.BasketCOurses.Remove(basketProduct);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
