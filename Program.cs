using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.DataAccsess;
using Uniqlo2.Extensions;
using Uniqlo2.Models;

namespace Uniqlo2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews ();
            builder.Services.AddDbContext<UniqloDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloDbContext>();
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.AccessDeniedPath = "/Home/AccessDenied/";
                

            });
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseUserSeed();
            app.MapControllerRoute(name: "register",
                pattern: "register",
                defaults: new { controller = "Account", action = "Register" });

            app.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
