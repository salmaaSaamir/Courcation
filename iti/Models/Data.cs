using iti.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using iti.ViewModelClasses;

namespace iti.Models
{
    public class Data :IdentityDbContext<ApplicationUser>
    {
        public Data() : base()
        {

        }
        public Data(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentsCourses> StudentCourses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CorsesCenter;Integrated Security=True");
        }
        public DbSet<iti.ViewModelClasses.RegisterUserViewModel>? RegisterUserViewModel { get; set; }
        public DbSet<iti.ViewModelClasses.LoginViewModel>? LoginViewModel { get; set; }
        public DbSet<iti.ViewModelClasses.RoleViewModel>? RoleViewModel { get; set; }
    }
}
