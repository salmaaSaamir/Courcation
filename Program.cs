using iti.Repos;
using iti.Models;
using iti.Repos;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using iti.Controllers;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Data>(optionBuilder => {
                optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("data"));
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<ICoursesRepo, CoursesRepo>();
            builder.Services.AddScoped<IStudentCoursesRepo, StudentCoursesRepo>();
            builder.Services.AddScoped<AccountController>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options=>
               {
                   options.Password.RequireDigit = true;    
                   options.Password.RequireUppercase = true;
               }
                
                ).AddEntityFrameworkStores<Data>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
