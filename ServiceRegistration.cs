using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Services;
using BackEndProject_Edu.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<EduDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            //services.AddScoped<ISumService, SumService>();
            //services.AddTransient<ISumService, SumService>();
            //services.AddSingleton<ISumService, SumService>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(15);
            });
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddHttpContextAccessor();
            services.AddSession();


            //todo:bunlar mesaji falan qoshmag uchundu,birinciler ise serviceler
            // services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password = new()
                {
                    RequiredLength = 8,
                    RequireUppercase = true,
                    RequireLowercase = true,
                    RequireDigit = true,
                    RequireNonAlphanumeric = true


                };
                options.Lockout = new()
                {
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5)
                };
                options.User = new()
                {
                    //todo:email confirm sondurmusen
                    RequireUniqueEmail = true,
                };
                //options.SignIn.RequireConfirmedEmail = true;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<EduDbContext>();
            //services.AddSignalR();
        }
    }
}
